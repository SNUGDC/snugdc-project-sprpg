using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BossAi
	{
		public bool IsRunning { get { return Running != null; } }
		public BossSkillActor Running { get; private set; }
		public bool IsBeforeDelaying { get { return _beforeDelayingLeft > 0; } }
		private Tick _beforeDelayingLeft;
		public bool CanPerform { get { return !IsBeforeDelaying && !IsRunning; } }

		private readonly Boss _boss;
		private readonly BossSkillFactory _skillFactory;

		public BossPhaseState PreviousPerformPhase { get; private set; }
		public bool WasPhaseChanged { get { return _boss.Phase != PreviousPerformPhase; } }

		public BossAi(Boss boss)
		{
			_boss = boss;
			_skillFactory = BossWholeSkillFactory.Map(boss);
			PreviousPerformPhase = (BossPhaseState) 1;
		}

		public void Tick()
		{
			if (IsBeforeDelaying)
				--_beforeDelayingLeft;
		}

		private BossSkillBalanceData Sample(Battle context)
		{
			var skills = _boss.Data.Skills;
			var sampler = new WeightedSampler<BossSkillBalanceData>();

			foreach (var kv in skills)
			{
				var data = kv.Value;
				if (data.SampleCondition.Test(context))
					sampler.Add(data.Weight, data);
			}

			return sampler.Sample(context.Random);
		}

		private BossSkillBalanceData SampleOrGetDebugSkillData(Battle context)
		{
			var useOnlySkill = DebugConfig.Battle.Boss.UseOnlySkill;
			if (useOnlySkill.HasValue)
			{
				BossSkillBalanceData data;
				if (_boss.Data.Skills.TryGetValue(useOnlySkill.Value, out data))
				{
					if (data.SampleCondition.Test(context))
						return data;
				}
			}

			return Sample(context);
		}

		public bool TryPerform(Battle context)
		{
			Debug.Assert(!IsRunning);

			if (IsBeforeDelaying)
			{
				Debug.LogError("now before delaying.");
				return false;
			}

			if (_boss.IsDead)
				return false;

			if (_boss.Data.Skills.Empty())
			{
				Debug.LogError("has no skill.");
				return false;
			}

			var data = SampleOrGetDebugSkillData(context);
			if (data == null)
				return false;

			PreviousPerformPhase = _boss.Phase;

			Running = _skillFactory.Create(data, context, _boss);
			Running.OnStop += OnStop;
			Running.Start();

			Events.Boss.OnSkillStart.CheckAndCall(_boss, Running);
			return true;
		}

		public void TryCancel()
		{
			if (!IsRunning) return;
			Running.Cancel();
		}

		private void ResetDelayTimers()
		{
			var phaseData = _boss.Data.Phases[(int) _boss.Phase.State - 1];
			_beforeDelayingLeft = (Tick)Random.Range((int)phaseData.DelayBeforeSkill[0], (int)phaseData.DelayBeforeSkill[1]);
		}

		public void ResetByStun()
		{
			TryCancel();
			ResetDelayTimers();
		}

		private void OnStop(BossSkillActor skill)
		{
			Debug.Assert(Running == skill);
			Running = null;
			ResetDelayTimers();
		}
	}
}
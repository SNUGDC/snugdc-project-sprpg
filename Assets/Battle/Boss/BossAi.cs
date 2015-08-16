using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BossAi
	{
		public bool IsPerforming { get { return Current != null; } }

		private readonly Boss _boss;
		private readonly BossSkillFactory _skillFactory;
		public BossSkillActor Current;

		public BossAi(Boss boss)
		{
			_boss = boss;
			_skillFactory = BossWholeSkillFactory.Map(boss);
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
			Debug.Assert(!IsPerforming);

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

			Current = _skillFactory.Create(data, context, _boss);
			Current.OnStop += OnStop;
			Current.Start();
			Events.Boss.OnSkillStart.CheckAndCall(_boss, Current);
			return true;
		}

		public void TryCancel()
		{
			if (!IsPerforming) return;
			Current.Cancel();
		}

		private void OnStop(BossSkillActor skill)
		{
			Debug.Assert(Current == skill);
			Current = null;
		}
	}
}
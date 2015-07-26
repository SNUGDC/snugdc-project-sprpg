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
				if (data.Condition.Test(context))
					sampler.Add(data.Weight, data);
			}

			return sampler.Sample(context.Random);
		}

		public void Perform(Battle context)
		{
			Debug.Assert(!IsPerforming);
			if (_boss.Data.Skills.Empty())
			{
				Debug.LogError("has no skill.");
				return;
			}

			var data = Sample(context);
			if (data == null)
				return;

			Current = _skillFactory.Create(context, _boss, data);
			Current.OnStop += OnStop;
			Current.Start();
			Events.OnBossSkillStart.CheckAndCall(_boss, Current);
		}

		private void OnStop(BossSkillActor skill)
		{
			Debug.Assert(Current == skill);
			Current = null;
		}
	}
}
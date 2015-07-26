using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BossAi
	{
		public bool IsPerforming { get { return _current != null; } }

		private readonly Boss _boss;
		private BossSkillActor _current;

		public BossAi(Boss boss)
		{
			_boss = boss;
		}

		private BossSkillBalanceData Sample(Battle context)
		{
			var skills = _boss.Data.Skills;
			var sampler = new WeightedSampler<BossSkillBalanceData>();

			foreach (var kv in skills)
			{
				var data = kv.Value;
				if (data.Condition.Test(context, _boss))
					sampler.Add(data.Proportion, data);
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
			_current = BossSkillFactory.Create(context, _boss, data);
			_current.OnStop += OnStop;
			_current.Start();	
		}

		private void OnStop(BossSkillActor skill)
		{
			Debug.Assert(_current == skill);
			_current = null;
		}
	}
}
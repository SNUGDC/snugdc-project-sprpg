﻿using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BossAi
	{
		public bool IsRunning { get { return Running != null; } }
		public BossSkillActor Running;
		private readonly Boss _boss;
		private readonly BossSkillFactory _skillFactory;

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
			Debug.Assert(!IsRunning);

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

		private void OnStop(BossSkillActor skill)
		{
			Debug.Assert(Running == skill);
			Running = null;
		}
	}
}
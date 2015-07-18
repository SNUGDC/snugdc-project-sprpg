using System;
using Gem;

namespace SPRPG.Battle
{
	public abstract class BossSkillActor : SkillActorBase
	{
		protected readonly Battle Context;
		protected readonly Boss Boss;
		protected readonly BossSkillBalanceData Data;

		public new Action<BossSkillActor> OnStop;

		protected BossSkillActor(Battle context, Boss boss, BossSkillBalanceData data)
		{
			Context = context;
			Boss = boss;
			Data = data;

			base.OnStop += skillActor => OnStop.CheckAndCall((BossSkillActor)skillActor);
		}
	}
}

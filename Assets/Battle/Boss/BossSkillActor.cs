using System;
using Gem;

namespace SPRPG.Battle
{
	public abstract class BossSkillActor : SkillActorBase
	{
		private readonly Battle _context;
		protected Battle Context { get { return _context; } }
		private readonly Boss _boss;
		protected Boss Boss { get { return _boss; } }
		private readonly BossSkillBalanceData _data;
		protected BossSkillBalanceData Data { get { return _data; } }

		public new Action<BossSkillActor> OnStop;

		protected BossSkillActor(Battle context, Boss boss, BossSkillBalanceData data)
		{
			_context = context;
			_boss = boss;
			_data = data;

			base.OnStop += skillActor => OnStop.CheckAndCall((BossSkillActor)skillActor);
		}
	}
}

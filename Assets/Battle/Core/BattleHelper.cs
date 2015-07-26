using System;

namespace SPRPG.Battle 
{
	public static partial class BattleHelper
	{
		public static Job AddPlayerPerform(this Battle thiz, Tick delay, Action callback)
		{
			return new Job(thiz.Fsm.PlayerPerform.Schedule, delay, callback);
		}

		public static Job AddBossPerform(this Battle thiz, Tick delay, Action callback)
		{
			return new Job(thiz.Fsm.BossPerform.Schedule, delay, callback);
		}

		public static Job AddAfterTurn(this Battle thiz, Tick delay, Action callback)
		{
			return new Job(thiz.Fsm.AfterTurn.Schedule, delay, callback);
		}
	}
}

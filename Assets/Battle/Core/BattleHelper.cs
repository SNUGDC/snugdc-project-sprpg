using System;

namespace SPRPG.Battle 
{
	public static partial class BattleHelper
	{
		public static Job AddPlayerSkill(this Battle thiz, Tick delay, Action callback)
		{
			return new Job(thiz.Fsm.PlayerSkill.Schedule, delay, callback);
		}

		public static Job AddBeforeTurn(this Battle thiz, Tick delay, Action callback)
		{
			return new Job(thiz.Fsm.BeforeTurn.Schedule, delay, callback);
		}

		public static Job AddAfterTurn(this Battle thiz, Tick delay, Action callback)
		{
			return new Job(thiz.Fsm.AfterTurn.Schedule, delay, callback);
		}

		public static void HitLeader(this Party thiz, Damage damage)
		{
			thiz.Leader.Hit(damage);
		}

		public static void HitParty(this Party thiz, Damage damage)
		{
			foreach (var member in thiz)
				member.Hit(damage);
		}
	}
}

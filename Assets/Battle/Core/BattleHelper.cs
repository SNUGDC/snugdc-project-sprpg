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

		public static void HitLeader(this Party thiz, Damage damage)
		{
			thiz.Leader.Hit(damage);
		}

		public static bool HitLeaderOrMemberIfLeaderIsDead(this Party thiz, Damage damage)
		{
			if (thiz.Leader.IsAlive)
			{
				thiz.Leader.Hit(damage);
				return true;
			}

			foreach (var member in thiz)
			{
				if (!member.IsAlive) continue;
				member.Hit(damage);
				return true;
			}

			return false;
		}

		public static void HitParty(this Party thiz, Damage damage)
		{
			foreach (var member in thiz)
				member.Hit(damage);
		}
	}
}

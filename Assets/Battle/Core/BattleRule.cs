namespace SPRPG.Battle
{
	public static class BattleRule
	{
		public static bool CheckWin(Battle battle)
		{
			return !battle.Boss.IsAlive;
		}

		public static bool CheckLose(Battle battle)
		{
			return !battle.Party.IsSomeoneAlive();
		}
	}
}

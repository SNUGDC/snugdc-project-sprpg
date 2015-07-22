namespace SPRPG
{
	public static class UserUtil
	{
		public static Battle.BattleDef MakeBattleDef(StageId stage)
		{
			return new Battle.BattleDef(stage, Party._.MakeDef());
		}
	}
}
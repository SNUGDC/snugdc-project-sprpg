namespace SPRPG.Battle
{
	public static class BossFactory
	{
		public static Boss Create(Battle context, BossId id)
		{
			return new Boss(context, BossBalance._.Find(id));
		}

		public static BossAi CreateAi(Boss boss)
		{
			return new BossAi(boss);
		}
	}
}

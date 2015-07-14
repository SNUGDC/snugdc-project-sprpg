namespace SPRPG.Battle
{
	public static class BossFactory
	{
		public static Boss Create(BossId id)
		{
			return new Boss(BossBalance._.Find(id));
		}

		public static BossAi CreateAi(Boss boss)
		{
			return new BossAi(boss);
		}
	}
}

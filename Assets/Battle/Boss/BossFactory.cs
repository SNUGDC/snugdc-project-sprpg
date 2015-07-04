namespace SPRPG.Battle
{
	public static class BossFactory
	{
		public static Boss Create(BossId id)
		{
			return new Boss((Hp) 100);
		}
	}
}

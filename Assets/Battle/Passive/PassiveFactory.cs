namespace SPRPG.Battle
{
	public static class PassiveFactory
	{
		public static Passive Create(PassiveKey key)
		{
			return new Passive();
		}
	}
}

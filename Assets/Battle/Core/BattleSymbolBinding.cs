namespace SPRPG.Battle
{
	public static class BattleSymbolBinding
	{
		public static SymbolBinding Create(Battle context)
		{
			var ret = new SymbolBinding();
			ret.Bind("BossHpPercentage", () => (int) context.Boss.HpPercentage);
			return ret;
		}
	}
}

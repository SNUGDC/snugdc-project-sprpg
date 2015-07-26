namespace SPRPG.Battle 
{
	public class BossPassive
	{
		public Tick TotalElapsed { get; private set; }

		protected readonly Battle Context;
		protected readonly Boss Boss;
		protected readonly BossPassiveBalanceData Data;

		protected BossPassive(Battle context, Boss boss, BossPassiveBalanceData data)
		{
			Context = context;
			Boss = boss;
			Data = data;
		}

		public bool Test()
		{
			return Data.ActivateCondition.Test(Context);
		}
		
		public void Tick()
		{
			++TotalElapsed;
			DoTick();
		}	

		protected virtual void DoTick() { }
	}

	public class BossNonePassive : BossPassive
	{
		public BossNonePassive(Battle context, Boss boss) : base(context, boss, new BossPassiveBalanceData { Key = BossPassiveLocalKey.None, })
		{ }
	}
}

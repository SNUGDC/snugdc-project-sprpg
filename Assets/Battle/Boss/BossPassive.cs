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

	public class BossTogglePassive : BossPassive
	{
		public bool WasActivated { get; private set; } 
		public bool? WasToggled { get; private set; }
		private readonly Condition _activateCondition;

		public BossTogglePassive(Battle context, Boss boss, BossPassiveBalanceData data) : base(context, boss, data)
		{
			_activateCondition = ConditionFactory.Create(data.Arguments["ActivateCondition"]);
		}

		public bool IsActivated()
		{
			return _activateCondition.Test(Context);
		}

		protected override void DoTick()
		{
			WasToggled = null;
			var isActivated = IsActivated();
			if (isActivated != WasActivated)
				Toggle(isActivated);
			base.DoTick();
			WasActivated = isActivated;
		}

		private void Toggle(bool val)
		{
			WasToggled = val;
			if (val) ToggleOn();
			else ToggleOff();	
		}

		protected virtual void ToggleOn() { }
		protected virtual void ToggleOff() { }
	}
}

using UnityEngine;

namespace SPRPG.Battle 
{
	public abstract class BossPassive
	{
		public Tick TotalElapsed { get; private set; }

		protected readonly Battle Context;
		protected readonly Boss Owner;
		protected readonly BossPassiveBalanceData Data;

		public bool WasActivated { get; private set; } 
		public bool? WasToggled { get; private set; }

		protected BossPassive(BossPassiveBalanceData data, Battle context, Boss owner)
		{
			Context = context;
			Owner = owner;
			Data = data;
		}

		public bool IsActivated()
		{
			if (Data.ActivateCondition == null) return true;
			return Data.ActivateCondition.Test(Context);
		}

		public void Tick()
		{
			++TotalElapsed;
			WasToggled = null;
			var isActivated = IsActivated();
			if (isActivated != WasActivated)
				Toggle(isActivated);
			DoTick();
			WasActivated = isActivated;
		}	

		protected virtual void DoTick() { }

		private void Toggle(bool val)
		{
			WasToggled = val;
			if (val) ToggleOn();
			else ToggleOff();	
		}

		protected virtual void ToggleOn() { }
		protected virtual void ToggleOff() { }

		public abstract void ResetByStun();
	}

	public sealed class BossNonePassive : BossPassive
	{
		public BossNonePassive(Battle context, Boss owner) : base(new BossPassiveBalanceData { Key = BossPassiveLocalKey.None, }, context, owner)
		{ }

		public override void ResetByStun() { }
	}

	public sealed class BossTimescalePassive : BossPassive
	{
		private readonly float _timescale;

		public BossTimescalePassive(BossPassiveBalanceData data, Battle context, Boss owner) : base(data, context, owner)
		{
			_timescale = (float) data.Arguments["Timescale"];
		}

		protected override void ToggleOn()
		{
			base.ToggleOn();
			Time.timeScale *= _timescale;
		}

		protected override void ToggleOff()
		{
			base.ToggleOff();
			Time.timeScale /= _timescale;
		}

		public override void ResetByStun() { }
	}
}

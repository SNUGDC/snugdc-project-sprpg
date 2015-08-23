using System;
using Gem;

namespace SPRPG.Battle
{
	public enum BossPhaseState { }

	public class BossPhase
	{
		private readonly Battle _context;
		private readonly BossBalanceData _data;
		public BossPhaseState State { get; private set; }
		public Action<BossPhaseState> OnChanged;

		public BossPhase(Battle context, BossBalanceData data)
		{
			_context = context;
			_data = data;
			State = (BossPhaseState) 1;
		}

		public void Update()
		{
			if ((int) State >= _data.Phases.Count) return;
			var proceedCondition = _data.Phases[(int) State].ProceedCondition;
			if (!proceedCondition.Test(_context)) return;
			Proceed();
		}

		private void Proceed()
		{
			++State;
			OnChanged.CheckAndCall(State);
		}

		public static implicit operator BossPhaseState(BossPhase thiz)
		{
			return thiz.State;
		}
	}

	public static partial class BossHelper
	{
		public static int ToIndex(this BossPhaseState thiz)
		{
			return (int)thiz - 1;
		}

		public static BossPhaseState MakeBossPhaseFromIndex(int i)
		{
			return (BossPhaseState)(i + 1);
		}
	}
}

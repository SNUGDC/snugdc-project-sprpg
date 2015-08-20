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
			if ((int) State > _data.PhaseProceedCondition.Count) return;
			var proceedCondition = _data.PhaseProceedCondition[(int) State - 1];
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
}

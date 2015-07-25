using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleFsm
	{
		private readonly Battle _context;

		public BattlePhase Current { get; private set; }

		public readonly BattlePhase Idle;
		public readonly BeforeTurnPhase BeforeTurn;
		public readonly AfterTurnPhase AfterTurn;
		public readonly InputPhase Input;
		public readonly PlayerPassivePhase PlayerPassive;
		public readonly BattlePhase BossPassive;
		public readonly BattlePhase PlayerPerform;
		public readonly BossPerformPhase BossPerform;
		public readonly BattlePhase ResultWon;
		public readonly BattlePhase ResultLost;

		public bool IsIdle { get { return Current == Idle; } }
		public bool IsResult { get { return Current == ResultWon || Current == ResultLost; } }

		public BattleFsm(Battle context)
		{
			_context = context;

			Idle = new BattlePhase(context, BattleState.Idle);
			BeforeTurn = new BeforeTurnPhase(context);
			AfterTurn = new AfterTurnPhase(context);
			Input = new InputPhase(context);
			PlayerPassive = new PlayerPassivePhase(context);
			BossPassive = new BattlePhase(context, BattleState.BossPassive);
			PlayerPerform = new BattlePhase(context, BattleState.PlayerPerform);
			BossPerform = new BossPerformPhase(context);
			ResultWon = new ResultWonPhase(context);
			ResultLost = new ResultLostPhase(context);

			Current = Idle;
			Current.Enter();
		}

		private bool CheckAndTransferToResult()
		{
			Debug.Assert(!IsResult, "phase is already result.");

			if (BattleRule.CheckLose(_context))
			{
				Transfer(ResultLost);
				return true;
			}

			if (BattleRule.CheckWin(_context))
			{
				Transfer(ResultWon);
				return true;
			}

			return false;
		}

		private void Transfer(BattlePhase phase)
		{
			Current.Exit();
			Current = phase;
			Current.Enter();
		}

		private BattlePhase GetNextPhase()
		{
			switch (Current.State)
			{
				case BattleState.Idle: return BeforeTurn;
				case BattleState.BeforeTurn: return Input;
				case BattleState.Input: return PlayerPassive;
				case BattleState.PlayerPassive: return BossPassive;
				case BattleState.BossPassive: return PlayerPerform;
				case BattleState.PlayerPerform: return BossPerform;
				case BattleState.BossPerform: return AfterTurn;
				case BattleState.AfterTurn: return Idle;
				default:
					Debug.LogError(LogMessages.EnumUndefined(Current.State));
					return Idle;
			}
		}

		private void Proceed()
		{
			if (IsResult) return;
			if (CheckAndTransferToResult()) return;
			Transfer(GetNextPhase());
		}

		public void Cycle()
		{
			if (Current != Idle)
			{
				Debug.LogError("current state should be idle.");
				return;
			}

			do
			{
				Proceed();
			}
			while (!IsIdle && !IsResult);
		}
	}
}

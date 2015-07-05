using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleFsm
	{
		public BattlePhase Current { get; private set; }

		public readonly BattlePhase Idle;
		public readonly BattlePhase BeforeTurn;
		public readonly BattlePhase AfterTurn;
		public readonly BattlePhase PlayerPassive;
		public readonly BattlePhase BossPassive;
		public readonly BattlePhase PlayerPerform;
		public readonly BattlePhase BossPerform;
		public readonly BattlePhase ResultWon;
		public readonly BattlePhase ResultLost;

		public bool IsResult { get { return Current == ResultWon || Current == ResultLost; } }

		public BattleFsm(Battle context)
		{
			Idle = new BattlePhase(context, BattleState.Idle);
			BeforeTurn = new BattlePhase(context, BattleState.BeforeTurn);
			AfterTurn = new BattlePhase(context, BattleState.AfterTurn);
			PlayerPassive = new BattlePhase(context, BattleState.PlayerPassive);
			BossPassive = new BattlePhase(context, BattleState.BossPassive);
			PlayerPerform = new BattlePhase(context, BattleState.PlayerPerform);
			BossPerform = new BattlePhase(context, BattleState.BossPerform);
			ResultWon = new BattlePhase(context, BattleState.ResultWon);
			ResultLost = new BattlePhase(context, BattleState.ResultLost);

			Current = Idle;
			Current.Enter();
		}

		private bool CheckAndTransferToResult()
		{
			// todo
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
				case BattleState.BeforeTurn: return PlayerPassive;
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
			while (Current != Idle);
		}
	}
}

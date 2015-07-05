﻿namespace SPRPG.Battle
{
	public enum BattleState
	{
		Idle,
		BeforeTurn,
		AfterTurn,
		PlayerPassive,
		BossPassive,
		PlayerPerform,
		BossPerform,
		ResultWon,
		ResultLost,
	}

	public class BattlePhase
	{
		private readonly BattleState _state;
		public BattleState State { get { return _state; } }

		private readonly Battle _context;
		private readonly Schedule _schedule;
		public Schedule Schedule { get { return _schedule; } }

		public BattlePhase(Battle context, BattleState state)
		{
			_state = state;
			_context = context;
			_schedule = new Schedule(context.Clock);
		}

		public virtual void Enter()
		{
			_schedule.Sync();
		}

		public virtual void Exit()
		{
			
		}
	}

	public static class BattlePhaseFactory
	{
		public static BattlePhase Create(Battle context, BattleState state)
		{
			return new BattlePhase(context, state);
		}
	}
}

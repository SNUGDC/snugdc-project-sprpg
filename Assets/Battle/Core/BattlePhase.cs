namespace SPRPG.Battle
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
		private readonly SchedulerJobQueue _jobQueue;
		public SchedulerJobQueue JobQueue { get { return _jobQueue; } }

		public BattlePhase(Battle context, BattleState state)
		{
			_state = state;
			_context = context;
			_jobQueue = new SchedulerJobQueue(context.Scheduler);
		}

		public virtual void Enter()
		{
			_jobQueue.Sync();
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

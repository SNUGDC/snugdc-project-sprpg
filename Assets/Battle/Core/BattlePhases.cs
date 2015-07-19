using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BeforeTurnPhase : BattlePhase
	{
		public BeforeTurnPhase(Battle context) : base(context, BattleState.BeforeTurn)
		{}

		public override void Enter()
		{
			Context.Party.BeforeTurn();
			base.Enter();
		}
	}

	public class AfterTurnPhase : BattlePhase
	{
		public AfterTurnPhase(Battle context) : base(context, BattleState.AfterTurn)
		{ }

		public override void Enter()
		{
			base.Enter();
			Context.Party.AfterTurn();
			Events.AfterTurn.CheckAndCall();
		}
	}

	public class BossPerformPhase : BattlePhase
	{
		public BossPerformPhase(Battle context) : base(context, BattleState.BossPerform)
		{
		}

		public override void Enter()
		{
			if (!Context.BossAi.IsPerforming)
				Context.BossAi.Perform(Context);
			base.Enter();
		}
	}

	public class ResultWonPhase : BattlePhase
	{
		public ResultWonPhase(Battle context) : base(context, BattleState.ResultWon)
		{ }

		public override void Enter()
		{
			base.Enter();
			Debug.Log("result: won");
		}
	}

	public class ResultLostPhase : BattlePhase
	{
		public ResultLostPhase(Battle context)
			: base(context, BattleState.ResultLost)
		{ }

		public override void Enter()
		{
			base.Enter();
			Debug.Log("result: lost");
		}
	}
}

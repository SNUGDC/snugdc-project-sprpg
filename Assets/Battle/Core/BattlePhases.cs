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

	public class InputPhase : BattlePhase
	{
		private Party _party { get { return Context.Party; } }

		public InputPhase(Battle context) : base(context, BattleState.Input)
		{}

		public override void Enter()
		{
			base.Enter();
			// first perform skill.
			var skillPerformed = TrySkill();
			// if skill is not performed, then perform shift.
			if (!skillPerformed) TryShift();

			var receiver = Context.InputReceiver;
			if (receiver.IsSomeReceived) 
				Context.PlayerClock.Rebase();
			receiver.Invalidate();
		}

		private bool TrySkill()
		{
			var skill = Context.InputReceiver.Skill;
			if (!skill.HasValue) return false;

			var termAndGrade = skill.Value;
			Debug.Log(termAndGrade);
			if (termAndGrade.Grade == InputGrade.Bad) return true;

			_party.Leader.PerformSkill(termAndGrade.Term.ToSkillSlot());
			return true;
		}

		private bool TryShift()
		{
			var shift = Context.InputReceiver.Shift;
			if (!shift.HasValue) return false;

			var termAndGrade = shift.Value;
			Debug.Log(termAndGrade);
			if (termAndGrade.Grade == InputGrade.Bad) return true;

			_party.Shift();
			return true;
		}
	}

	public class PlayerPassivePhase : BattlePhase
	{
		public PlayerPassivePhase(Battle context) : base(context, BattleState.PlayerPassive)
		{
		}

		public override void Enter()
		{
			foreach (var member in Context.Party)
				member.Passive.Tick();
			base.Enter();
		}
	}

	public class BossPassivePhase : BattlePhase
	{
		public BossPassivePhase(Battle context) : base(context, BattleState.BossPassive)
		{
		}

		public override void Enter()
		{
			Context.Boss.TickPassive();
			base.Enter();
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
			Events.OnWin.CheckAndCall();
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
			Events.OnLose.CheckAndCall();
		}
	}
}

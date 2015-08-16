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
			Context.Boss.AfterTurn();
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
			Events.OnInputSkill.CheckAndCall(termAndGrade);
			if (termAndGrade.IsGradeBad) return true;

			var skillSlot =	termAndGrade.Term.ToSkillSlot();
			var skillActor = _party.Leader.TryPerformSkill(skillSlot);

			var leaderIdx = _party.LeaderIdx;
			Events.GetCharacter(leaderIdx).OnSkillStart.CheckAndCall(leaderIdx, _party.Leader, skillActor);
			return true;
		}

		private bool TryShift()
		{
			var shift = Context.InputReceiver.Shift;
			if (!shift.HasValue) return false;

			var termAndGrade = shift.Value;
			Events.OnInputShift.CheckAndCall(termAndGrade);
			if (termAndGrade.IsGradeBad) return true;

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
				member.TickPassive();
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

	public class BossSkillPhase : BattlePhase
	{
		public BossSkillPhase(Battle context) : base(context, BattleState.BossSkill)
		{
		}

		public override void Enter()
		{
			if (Context.Boss.Ai.CanPerform)
				Context.Boss.Ai.TryPerform(Context);
			Context.Boss.Ai.Tick();
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

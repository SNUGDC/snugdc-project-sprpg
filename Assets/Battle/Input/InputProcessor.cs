using System;
using Gem;

namespace SPRPG.Battle
{
	public class InputProcessor
	{
		protected readonly InputReceiver Receiver;
		private readonly Scheduler _scheduler;
		public Action<Term, InputGrade> OnInvoke;
		
		public InputProcessor(InputReceiver receiver, Scheduler scheduler)
		{
			Receiver = receiver;
			_scheduler = scheduler;
		}

		protected void OnInput()
		{
			RebaseAndInvoke();
		}

		private static InputGrade GradeInput(Tick distance)
		{
			var validBefore = Config.Data.Battle.InputValidBefore;
			var validAfter = Config.Data.Battle.InputValidAfter;
			var distanceInt = (int)distance;

			if (distanceInt < -validBefore || distanceInt > validAfter)
				return InputGrade.Bad;
			return InputGrade.Good;
		}

		private void RebaseAndInvoke()
		{
			if ((int) _scheduler.Relative < (int) Const.Term/2)
			{
				_scheduler.Rebase();
				OnInvoke.CheckAndCall(Term._1, InputGrade.Bad);
				return;
			}

			var termAndDistance = _scheduler.GetCloseTermAndDistance();
			_scheduler.Rebase();
			var inputGrade = GradeInput(termAndDistance.Distance);
			OnInvoke.CheckAndCall(termAndDistance.Term, inputGrade);
		}
	}

	public sealed class SkillInputProcessor : InputProcessor
	{
		public SkillInputProcessor(InputReceiver receiver, Scheduler scheduler)
			: base(receiver, scheduler)
		{
			Receiver.OnSkill += OnInput;
		}

		~SkillInputProcessor()
		{
			Receiver.OnSkill -= OnInput;
		}
	}

	public class ShiftInputProcessor : InputProcessor
	{
		public ShiftInputProcessor(InputReceiver receiver, Scheduler scheduler) 
			: base(receiver, scheduler)
		{
			Receiver.OnShift += OnInput;
		}

		~ShiftInputProcessor()
		{
			Receiver.OnShift -= OnInput;
		}
	}
}

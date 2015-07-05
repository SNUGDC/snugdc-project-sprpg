using System;
using Gem;

namespace SPRPG.Battle
{
	public class InputProcessor
	{
		protected readonly InputReceiver Receiver;
		private readonly RelativeClock _clock;
		public Action<Term, InputGrade> OnInvoke;
		
		public InputProcessor(InputReceiver receiver, RelativeClock clock)
		{
			Receiver = receiver;
			_clock = clock;
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
			if ((int) _clock.Relative < (int) Const.Term/2)
			{
				_clock.Rebase();
				OnInvoke.CheckAndCall(Term._1, InputGrade.Bad);
				return;
			}

			var termAndDistance = _clock.GetCloseTermAndDistance();
			_clock.Rebase();
			var inputGrade = GradeInput(termAndDistance.Distance);
			OnInvoke.CheckAndCall(termAndDistance.Term, inputGrade);
		}
	}

	public sealed class SkillInputProcessor : InputProcessor
	{
		public SkillInputProcessor(InputReceiver receiver, RelativeClock clock)
			: base(receiver, clock)
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
		public ShiftInputProcessor(InputReceiver receiver, RelativeClock clock) 
			: base(receiver, clock)
		{
			Receiver.OnShift += OnInput;
		}

		~ShiftInputProcessor()
		{
			Receiver.OnShift -= OnInput;
		}
	}
}

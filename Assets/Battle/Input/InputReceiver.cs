using UnityEngine;

namespace SPRPG.Battle
{
	public enum InputReceiverType
	{
#if KEYBOARD_SUPPORTED
		Keyboard,
#endif

#if TOUCH_SUPPORTED
		Touch,
#endif
	}

	public struct TermAndGrade
	{
		public Term Term;
		public InputGrade Grade;

		public TermAndGrade(Term term, InputGrade grade)
		{
			Term = term;
			Grade = grade;
		}

		public override string ToString()
		{
			return "term: " + Term + ", grade: " + Grade;
		}
	}

	public abstract class InputReceiver
	{
		public bool IsSomeReceived { get { return Skill.HasValue || Shift.HasValue; } }

		public TermAndGrade? Skill { get; private set; }
		public TermAndGrade? Shift { get; private set; }

		public abstract void Update(RelativeClock clock, float dt);

		public void Invalidate()
		{
			Skill = null;
			Shift = null;
		}

		public void ForceCaptureSkill(RelativeClock clock)
		{
			CaptureSkill(clock);
		}

		public void ForceCaptureShift(RelativeClock clock)
		{
			CaptureShift(clock);
		}

		protected void CaptureSkill(RelativeClock clock) { Skill = CaptureInput(clock); }
		protected void CaptureShift(RelativeClock clock) { Shift = CaptureInput(clock); }

		private static bool CheckInputWasTooEarly(Tick tick)
		{
			return (int)tick < (int)Const.Term / 2;
		}

		private static TermAndGrade CaptureInput(RelativeClock clock)
		{
			var termAndDistance = clock.GetCloseTermAndDistance();
			if (CheckInputWasTooEarly(clock.Relative)) 
				return new TermAndGrade(termAndDistance.Term, InputGrade.Bad);
			return new TermAndGrade(termAndDistance.Term, GradeInput(termAndDistance.Distance));
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
	}

#if KEYBOARD_SUPPORTED
	public sealed class KeyboardInputReceiver : InputReceiver
	{
		private const KeyCode SkillKey = KeyCode.X;
		private const KeyCode ShiftKey = KeyCode.Z;

		public override void Update(RelativeClock clock, float dt)
		{
			if (Input.GetKeyDown(SkillKey))
				CaptureSkill(clock);
			if (Input.GetKeyDown(ShiftKey))
				CaptureShift(clock);
		}
	}
#endif

#if TOUCH_SUPPORTED
	public sealed class TouchInputReceiver : InputReceiver
	{
		public override void Update(RelativeClock clock, float dt)
		{
			foreach (var touch in Input.touches)
			{
				if (touch.phase == TouchPhase.Began)
				{
					Debug.Log(touch.position.ToString());
				}
			}
		}
	}
#endif

	public static class InputReceiverFactory
	{
		public static InputReceiver Create(InputReceiverType type)
		{
			switch (type)
			{
#if KEYBOARD_SUPPORTED
				case InputReceiverType.Keyboard:
					return new KeyboardInputReceiver();
#endif
#if TOUCH_SUPPORTED
				case InputReceiverType.Touch:
					return new TouchInputReceiver();
#endif
			}

			return null;
		}

		public static InputReceiver CreatePlatformPreferred()
		{
#if (UNITY_ANDROID || UNITY_IOS)
#if TOUCH_SUPPORTED
			return new TouchInputReceiver();
#endif
#elif KEYBOARD_SUPPORTED
			return new KeyboardInputReceiver();
#else
#error undefined platform
#endif
		}
	}
}
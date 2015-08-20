using System;
using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public struct TermAndDistance
	{
		public Term Term;
		public Tick Distance;

		public TermAndDistance(Term term, Tick distance)
		{
			Term = term;
			Distance = distance;
		}

		public TermAndDistance(Tick tick)
		{
			if (tick < 0)
			{
				Distance = tick;
				Term = Term._1;
			}
			else
			{
				Distance = (Tick)((int)tick % (int)Const.Term);
				Term = (Term)(((int)tick / (int)Const.Term) % SPRPG.Const.SkillSlotSize);
			}
		}
	}

	public class Clock
	{
		public const Tick InitialTick = (Tick)(-15);

		public Tick Current { get; private set; }

		public Action<Clock> OnProceed;

		public Clock()
		{
			Current = InitialTick;
		}

		public void Proceed()
		{
			++Current;
			OnProceed.CheckAndCall(this);
		}
	}

	public class RelativeClock
	{
		private readonly Clock _clock;

		public Tick Base { get; private set; }
		public Tick Current { get { return _clock.Current; } }

		public Tick Relative { get { return Current.Sub(Base); } }
		public Tick RelativePeriodic { get { return (Tick)((int) Relative%(int) Const.Period); } }

		public bool IsPerfectTerm { get { return ((int)Relative % (int)Const.Term) == 0; } }

		public Term CloseTerm
		{
			get
			{
				const int halfTerm = (int) Const.Term/2;
				var relative = (int)RelativePeriodic;
				if (relative < halfTerm)
					return Term._4;

				return (Term)((relative - halfTerm) / (int)Const.Term);
			}
		}

		public Period Period { get { return (Period) ((int) Current/(int) Const.Period); } }

		public RelativeClock(Clock clock)
		{
			_clock = clock;
		}

		public void Rebase()
		{
			Base = Current - CSharpHelper.ModPositive((int)Relative, (int)Const.Term) + (int)Const.Term;
		}

		public TermAndDistance GetCurrentTermAndDistance()
		{
			return new TermAndDistance(Relative);
		}
	}

	public static class ClockHelper
	{
		public static Tick Add(this Tick thiz, Tick other)
		{
			return (Tick)((int)thiz + (int)other);
		}

		public static Tick Sub(this Tick thiz, Tick other)
		{
			return (Tick)((int)thiz - (int)other);
		}

		public static Tick ToTick(this Term thiz)
		{
			return (Tick) ((int) thiz*(int) Const.Term);
		}

		public static Tick ToTick(this Period thiz)
		{
			return (Tick) ((int) thiz*(int) Const.Period);
		}

		public static SkillSlot ToSkillSlot(this Term thiz)
		{
			switch (thiz)
			{
				case Term._1: return SkillSlot._1;
				case Term._2: return SkillSlot._2;
				case Term._3: return SkillSlot._3;
				case Term._4: return SkillSlot._4;
			}

			Debug.LogError(LogMessages.EnumUndefined(thiz));
			return default(SkillSlot);
		}
	}
}
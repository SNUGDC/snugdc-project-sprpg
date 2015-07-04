using System;
using System.Collections.Generic;
using System.Diagnostics;
using Gem;

namespace SPRPG.Battle
{
	public enum Tick {}
	public enum Term { _1, _2, _3, _4 }
	public enum Period {}

	public class Scheduler
	{
		public enum JobId {}

		private struct Job
		{
			public JobId Id;
			public Action Callback;
		}

		private JobId _uniqueId = default(JobId);
		public Tick Base { get; private set; }
		public Tick Current { get; private set; }
		public Tick Relative { get { return Current.Sub(Base); } }

		public bool IsPerfectTerm { get { return ((int)Relative % (int)Const.Term) == 0; } }

		public Term CloseTerm
		{
			get
			{
				const int halfTerm = (int) Const.Term/2;
				var relative = (int)Relative;
				if (relative < halfTerm)
					return Term._4;
				return (Term)((relative - halfTerm) / (int)Const.Term);
			}
		}

		public Period Period { get { return (Period) ((int) Current/(int) Const.Period); } }

		private readonly Dictionary<Tick, List<Job>> _jobs = new Dictionary<Tick, List<Job>>();

		private JobId AssignUniqueId()
		{
			return _uniqueId++;
		}

		public JobId AddRelative(Tick tick, Action callback)
		{
			Debug.Assert(tick == default(Tick), "you should not add job to current tick.");
			Debug.Assert(callback != null, "callback is null.");

			var absolute = Current.Add(tick);

			var jobsTick = _jobs[absolute];
			if (jobsTick == null)
			{
				jobsTick = new List<Job>();
				_jobs[absolute] = jobsTick;
			}

			var jobId = AssignUniqueId();
			jobsTick.Add(new Job { Id = jobId, Callback = callback });
			return jobId;
		}

		public void Rebase()
		{
			Base = Current;
		}

		public void Proceed()
		{
			Debug.Assert(Relative < Const.Period);

			++Current;
			if (Relative >= Const.Period)
				Base = Period.ToTick();

			List<Job> jobsTick;
			if (!_jobs.TryGetAndRemove(Current, out jobsTick))
				return;

			foreach (var job in jobsTick)
				job.Callback();
		}
	}

	public static class SchedulerHelper
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
	}
}
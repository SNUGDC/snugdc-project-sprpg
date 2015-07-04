using System;
using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class SchedulerJobQueue
	{
		public enum JobId { }

		private struct Job
		{
			public JobId Id;
			public Action Callback;
		}

		private static JobId _uniqueId = default(JobId);
		private static JobId AssignUniqueId()
		{
			return _uniqueId++;
		}

		private readonly Scheduler _scheduler;
		private Tick _lastSync;
		private readonly Dictionary<Tick, List<Job>> _jobs = new Dictionary<Tick, List<Job>>();

		public SchedulerJobQueue(Scheduler scheduler)
		{
			_scheduler = scheduler;
			_lastSync = scheduler.Current;
		}

		public JobId AddRelative(Tick tick, Action callback)
		{
			if (tick == default(Tick))
			{
				Debug.Assert(tick == default(Tick), "you should not add job to current tick, anyway callback immediately.");
				return default(JobId);
			}

			Debug.Assert(callback != null, "callback is null.");

			var absolute = _scheduler.Current.Add(tick);

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

		public void Sync()
		{
			Debug.Assert(_lastSync == _scheduler.Current - 1);
			while (_lastSync < _scheduler.Current)
				InvokeAndRemove(++_lastSync);
		}

		private void InvokeAndRemove(Tick tick)
		{
			List<Job> jobsTick;
			if (!_jobs.TryGetAndRemove(tick, out jobsTick))
				return;

			foreach (var job in jobsTick)
				job.Callback();
		}
	}
}

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
			return ++_uniqueId;
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
			return AddAbsolte(_scheduler.Current.Add(tick), callback);
		}

		public JobId AddAbsolte(Tick tick, Action callback)
		{
			if (tick == _scheduler.Current)
			{
				Debug.LogError("you should not add job to current tick, anyway callback immediately.");
				return default(JobId);
			}

			Debug.Assert(callback != null, "callback is null.");

			var jobsTick = _jobs[tick];
			if (jobsTick == null)
			{
				jobsTick = new List<Job>();
				_jobs[tick] = jobsTick;
			}

			var jobId = AssignUniqueId();
			jobsTick.Add(new Job { Id = jobId, Callback = callback });
			return jobId;
		}

		public void Sync()
		{
			Debug.Assert(_lastSync == default(Tick) || _lastSync == _scheduler.Current - 1);
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

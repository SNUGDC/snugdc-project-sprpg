using System;
using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public enum JobId { }

	public class Schedule
	{
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

		private readonly Clock _clock;
		private Tick _lastSync;
		private readonly Dictionary<Tick, List<Job>> _jobs = new Dictionary<Tick, List<Job>>();
		private readonly Dictionary<JobId, Tick> _jobToTick = new Dictionary<JobId, Tick>();

		public Schedule(Clock clock)
		{
			_clock = clock;
			_lastSync = clock.Current;
		}

		public JobId AddRelative(Tick tick, Action callback)
		{
			return AddAbsolte(_clock.Current.Add(tick), callback);
		}

		public JobId AddAbsolte(Tick tick, Action callback)
		{
			if (tick == _clock.Current)
			{
				Debug.LogError("you should not add job to current tick, anyway callback immediately.");
				return default(JobId);
			}

			Debug.Assert(callback != null, "callback is null.");

			List<Job> jobsTick;
			if (!_jobs.TryGetValue(tick, out jobsTick))
			{
				jobsTick = new List<Job>();
				_jobs[tick] = jobsTick;
			}

			var jobId = AssignUniqueId();
			jobsTick.Add(new Job { Id = jobId, Callback = callback });
			_jobToTick.Add(jobId, tick);
			return jobId;
		}

		public bool Remove(JobId jobId)
		{
			Tick tick;
			if (!_jobToTick.TryGetAndRemove(jobId, out tick))
			{
				Debug.LogError("job " + jobId + " not found.");
				return false;
			}

			var result = _jobs[tick].RemoveIf(job => job.Id == jobId);
			Debug.Assert(result);
			return true;
		}

		public void Sync()
		{
			Debug.Assert(_lastSync == default(Tick) || _lastSync == _clock.Current - 1);
			while (_lastSync < _clock.Current)
				InvokeAndRemove(++_lastSync);
		}

		private void InvokeAndRemove(Tick tick)
		{
			List<Job> jobsTick;
			if (!_jobs.TryGetAndRemove(tick, out jobsTick))
				return;

			foreach (var job in jobsTick)
			{
				_jobToTick.Remove(job.Id);
				job.Callback();
			}
		}
	}
}

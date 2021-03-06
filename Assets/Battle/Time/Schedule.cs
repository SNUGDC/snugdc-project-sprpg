﻿using System;
using System.Collections.Generic;
using System.Linq;
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
		private readonly SortedDictionary<Tick, List<Job>> _jobs = new SortedDictionary<Tick, List<Job>>();
		private readonly SortedDictionary<JobId, Tick> _jobToTick = new SortedDictionary<JobId, Tick>();

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

		private Tick? FindTick(JobId jobId)
		{
			if (_jobToTick.Empty())
				return null;

			if (_jobToTick.First().Key > jobId)
				return null;

			Tick tick;
			if (!_jobToTick.TryGetAndRemove(jobId, out tick))
				return null;

			return tick;
		}

		public bool TryRemove(JobId jobId)
		{
			var tick = FindTick(jobId);
			if (!tick.HasValue) return false;
			var result = _jobs[tick.Value].RemoveIf(job => job.Id == jobId);
			Debug.Assert(result);
			return true;
		}

		public bool Remove(JobId jobId)
		{
			var ret = TryRemove(jobId);
			if (!ret) Debug.LogError("job " + jobId + " not found.");
			return ret;
		}

		public void Sync()
		{
			Debug.Assert(_lastSync == Clock.InitialTick || _lastSync == _clock.Current - 1);
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

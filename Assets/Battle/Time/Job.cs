using System;
using UnityEngine;

namespace SPRPG.Battle
{
	public class Job
	{
		public bool MustBeDone { get { return _jobId != default(JobId); } }

		private readonly Schedule _schedule;
		private JobId _jobId;

		public Job(Schedule schedule, Tick delay, Action callback)
		{
			_schedule = schedule;
			_jobId = _schedule.AddRelative(delay, callback);
		}

		public void Cancel()
		{
			if (MustBeDone)
			{
				Debug.LogError("already canceled");
				return;
			}

			_schedule.Remove(_jobId);
			SetAsDone();
		}

		public void SetAsDone()
		{
			_jobId = default(JobId);
		}
	}
}

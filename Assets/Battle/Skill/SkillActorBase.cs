using System;
using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public abstract class SkillActorBase
	{
		public bool IsRunning { get; private set; }

		public Action<SkillActorBase> OnStop;

		public void Start()
		{
			if (IsRunning)
			{
				Debug.LogError("already running.");
				return;
			}

			IsRunning = true;
			DoStart();
		}

		public void Stop()
		{
			if (!IsRunning)
			{
				Debug.LogError("not running.");
				return;
			}

			IsRunning = false;
			DoStop();
			OnStop.CheckAndCall(this);
		}

		public void Cancel()
		{
			if (!IsRunning)
			{
				Debug.LogError("not performing.");
				return;
			}

			DoCancel();
			Stop();
		}

		protected abstract void DoStart();
		protected virtual void DoStop() { }
		protected virtual void DoCancel() { }
	}
}

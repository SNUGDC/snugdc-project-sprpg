using System;
using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public abstract class BossSkillActor
	{
		private readonly Battle _context;
		protected Battle Context { get { return _context; } }
		private readonly Boss _boss;
		protected Boss Boss { get { return _boss; } }
		private readonly BossSkillBalanceData _data;
		protected BossSkillBalanceData Data { get { return _data; } }

		public bool IsPerforming { get; private set; }

		public Action<BossSkillActor> OnStop;

		protected BossSkillActor(Battle context, Boss boss, BossSkillBalanceData data)
		{
			_context = context;
			_boss = boss;
			_data = data;
		}

		public void Perform()
		{
			if (IsPerforming)
			{
				Debug.LogError("already performing.");
				return;
			}

			IsPerforming = true;
			DoPerform();
		}

		public void Stop()
		{
			if (!IsPerforming)
			{
				Debug.LogError("not performing.");
				return;
			}

			IsPerforming = false;
			DoStop();
			OnStop.CheckAndCall(this);
		}

		protected abstract void DoPerform();
		protected abstract void DoStop();
	}
}

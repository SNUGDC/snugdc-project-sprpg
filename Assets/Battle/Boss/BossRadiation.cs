using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class RadiationAttackSkillActor : BossSkillActor
	{
		private readonly Damage _damage;

		private JobId _hitJob;
		private JobId _stopJob;

		public RadiationAttackSkillActor(Battle context, Boss boss, BossSkillBalanceData data)
			: base(context, boss, data)
		{
			Debug.Assert(boss.Id == BossId.Radiation);
			_damage = Data.Arguments["Damage"].ToObject<Damage>();
		}

		protected override void DoStart()
		{
			// todo: extract value to json.
			_hitJob = Context.AddBossPerform((Tick)3, () =>
			{
				Context.Party.Leader.Hit(_damage);
				_hitJob = default(JobId);
			});

			_stopJob = Context.Fsm.AfterTurn.Schedule.AddRelative((Tick) 5, () =>
			{
				_stopJob = default(JobId);
				Stop();
			});
		}

		protected override void DoStop()
		{
			if (_hitJob != default(JobId))
				Context.RemoveBossPerform(_hitJob);

			if (_stopJob != default(JobId))
				Context.Fsm.AfterTurn.Schedule.Remove(_stopJob);
		}
	}
}

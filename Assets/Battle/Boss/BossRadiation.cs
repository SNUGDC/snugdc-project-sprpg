using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BossRadiationAttackSkillActor : BossSkillActor
	{
		private readonly Damage _damage;

		private Job _hitJob;
		private Job _stopJob;

		public BossRadiationAttackSkillActor(Battle context, Boss boss, BossSkillBalanceData data)
			: base(context, boss, data)
		{
			Debug.Assert(boss.Id == BossId.Radiation);
			_damage = Data.Arguments["Damage"].ToObject<Damage>();
		}

		protected override void DoStart()
		{
			_hitJob = Context.AddBossPerform((Tick)3, () => Context.Party.Leader.Hit(_damage));
			_stopJob = Context.AddAfterTurn((Tick) 5, Stop);
		}

		protected override void DoCancel()
		{
			_hitJob.Cancel();
			_stopJob.Cancel();
		}
	}
}

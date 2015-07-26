using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BossRadioactiveAreaPassive : BossPassive
	{
		public const Tick Cooltime = Const.Term;
		private readonly Cooltimer _cooltimer = new Cooltimer(Cooltime);
		private readonly BossDamageArgument _arguments;

		public BossRadioactiveAreaPassive(Battle context, Boss boss, BossPassiveBalanceData data) : base(context, boss, data)
		{
			_arguments = new BossDamageArgument(data.Arguments);
		}

		protected override void DoTick()
		{
			if (!_cooltimer.Tick()) return;
			Context.Party.HitParty(_arguments);
		}
	}

	public class BossRadiationAttackSkillActor : BossSkillActor
	{
		private readonly BossDamageArgument _damage;

		private Job _hitJob;
		private Job _stopJob;

		public BossRadiationAttackSkillActor(Battle context, Boss boss, BossSkillBalanceData data)
			: base(context, boss, data)
		{
			Debug.Assert(boss.Id == BossId.Radiation);
			_damage = new BossDamageArgument(data.Arguments);
		}

		protected override void DoStart()
		{
			_hitJob = Context.AddBossPerform((Tick)3, () => Context.Party.HitLeaderOrMemberIfLeaderIsDead(_damage));
			_stopJob = Context.AddAfterTurn((Tick) 5, Stop);
		}

		protected override void DoCancel()
		{
			_hitJob.Cancel();
			_stopJob.Cancel();
		}
	}

	public sealed class BossRadiationPassiveFactory : BossPassiveFactory
	{
		public static readonly BossRadiationPassiveFactory _ = new BossRadiationPassiveFactory();

		public override BossPassive Create(Battle context, Boss boss, BossPassiveBalanceData data)
		{
			Debug.Assert(boss.Id == BossId.Radiation);
			var key = data.Key;	
			switch (key)
			{
				case BossPassiveLocalKey.RadioactiveArea:
					return new BossRadioactiveAreaPassive(context, boss, data);
				default:
					Debug.LogError("boss " + key + "'s skill " + key + " not handled.");
					return new BossNonePassive(context, boss);
			}
		}
	}

	public sealed class BossRadiationSkillFactory : BossSkillFactory
	{
		public static BossRadiationSkillFactory _ = new BossRadiationSkillFactory();
		 
		public override BossSkillActor Create(Battle context, Boss boss, BossSkillBalanceData data)
		{
			Debug.Assert(boss.Id == BossId.Radiation);

			switch (data.Key)
			{
				case BossSkillLocalKey.Attack:
					return new BossRadiationAttackSkillActor(context, boss, data);
				default:
					Debug.LogError(LogMessages.EnumUndefined(data.Key));
					return new BossNoneSkillActor(context, boss);
			}
		}
	}
}

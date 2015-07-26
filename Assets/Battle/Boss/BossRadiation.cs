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
			if (!Test()) return;
			Context.Party.HitParty(_arguments);
		}
	}

	public class BossRadiationAttackSkillActor : BossSingleDelayedPerformSkillActor 
	{
		private readonly BossDamageArgument _damage;

		public BossRadiationAttackSkillActor(Battle context, Boss boss, BossSkillBalanceData data)
			: base(context, boss, data, (Tick)3)
		{
			Debug.Assert(boss.Id == BossId.Radiation);
			_damage = new BossDamageArgument(data.Arguments);
		}

		protected override void Perform()
		{
			Context.Party.HitLeaderOrMemberIfLeaderIsDead(_damage);
		}
	}

	public class BossRadiationMassAttackSkillActor : BossSingleDelayedPerformSkillActor 
	{
		private readonly BossDamageArgument _damage;

		public BossRadiationMassAttackSkillActor(Battle context, Boss boss, BossSkillBalanceData data)
			: base(context, boss, data, (Tick)3)
		{
			Debug.Assert(boss.Id == BossId.Radiation);
			_damage = new BossDamageArgument(data.Arguments);
		}

		protected override void Perform()
		{
			Context.Party.HitParty(_damage);
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
				case BossSkillLocalKey.MassAttack:
					return new BossRadiationMassAttackSkillActor(context, boss, data);
				default:
					Debug.LogError(LogMessages.EnumUndefined(data.Key));
					return new BossNoneSkillActor(context, boss);
			}
		}
	}
}

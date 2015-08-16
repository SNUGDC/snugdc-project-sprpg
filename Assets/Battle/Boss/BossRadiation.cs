using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public sealed class BossRadioactiveAreaPassive : BossPassive
	{
		public const Tick Cooltime = Const.Term;
		private readonly Cooltimer _cooltimer = new Cooltimer(Cooltime);
		private readonly BossDamageArgument _arguments;

		public BossRadioactiveAreaPassive(BossPassiveBalanceData data, Battle context, Boss owner) : base(data, context, owner)
		{
			_arguments = new BossDamageArgument(data.Arguments);
		}

		protected override void DoTick()
		{
			if (!_cooltimer.Tick()) return;
			Context.Party.HitParty(_arguments);
		}

		public override void ResetByStun()
		{
			_cooltimer.Reset();
		}
	}

	public sealed class BossRadiationStep1MeltDownPassive : BossPassive 
	{
		private const Tick HealCooltime = Const.Term;

		private readonly StatDamageModifier _damageModifier;
		private readonly Hp _heal;
		private readonly Cooltimer _cooltimer = new Cooltimer(HealCooltime);

		public BossRadiationStep1MeltDownPassive(BossPassiveBalanceData data, Battle context, Boss owner)
			: base(data, context, owner)
		{
			_damageModifier = (StatDamageModifier) (int) data.Arguments["DamageModifier"];
			_heal = (Hp) (int) data.Arguments["Heal"];
		}

		protected override void DoTick()
		{
			base.DoTick();

			if (WasActivated)
			{
				if (_cooltimer.Tick())
					Owner.Heal(_heal);
			}
		}

		protected override void ToggleOn()
		{
			base.ToggleOn();
			Owner.Stats.AddDamageModifier(_damageModifier);
		}

		protected override void ToggleOff()
		{
			base.ToggleOff();
			Owner.Stats.RemoveDamageModifier(_damageModifier);
		}

		public override void ResetByStun()
		{
			_cooltimer.Reset();
		}
	}

	public class BossRadiationAttackSkillActor : BossSingleDelayedPerformSkillActor 
	{
		private readonly BossDamageArgument _damage;

		public BossRadiationAttackSkillActor(BossSkillBalanceData data, Battle context, Boss owner)
			: base(data, context, owner, (Tick)3)
		{
			Debug.Assert(owner.Id == BossId.Radiation);
			_damage = new BossDamageArgument(data.Arguments);
		}

		protected override void Perform()
		{
			var damageModified = Owner.ApplyModifier(_damage);
			Context.Party.HitLeaderOrMemberIfLeaderIsDead(damageModified);
		}
	}

	public class BossRadiationMassAttackSkillActor : BossSingleDelayedPerformSkillActor 
	{
		private readonly BossDamageArgument _damage;

		public BossRadiationMassAttackSkillActor(BossSkillBalanceData data, Battle context, Boss owner)
			: base(data, context, owner, (Tick)3)
		{
			Debug.Assert(owner.Id == BossId.Radiation);
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

		public override BossPassive Create(BossPassiveBalanceData data, Battle context, Boss owner)
		{
			Debug.Assert(owner.Id == BossId.Radiation);
			var key = data.Key;	
			switch (key)
			{
				case BossPassiveLocalKey.RadioactiveArea:
					return new BossRadioactiveAreaPassive(data, context, owner);
				case BossPassiveLocalKey.Step1MeltDown:
					return new BossRadiationStep1MeltDownPassive(data, context, owner);
				default:
					Debug.LogError("boss " + key + "'s skill " + key + " not handled.");
					return new BossNonePassive(context, owner);
			}
		}
	}

	public sealed class BossRadiationSkillFactory : BossSkillFactory
	{
		public static BossRadiationSkillFactory _ = new BossRadiationSkillFactory();
		 
		public override BossSkillActor Create(BossSkillBalanceData data, Battle context, Boss owner)
		{
			Debug.Assert(owner.Id == BossId.Radiation);

			switch (data.Key)
			{
				case BossSkillLocalKey.Attack:
					return new BossRadiationAttackSkillActor(data, context, owner);
				case BossSkillLocalKey.MassAttack:
					return new BossRadiationMassAttackSkillActor(data, context, owner);
				case BossSkillLocalKey.Radioactivity:
					return new BossGrantStatusConditionSkillActor(data, context, owner);
				default:
					Debug.LogError(LogMessages.EnumUndefined(data.Key));
					return new BossNoneSkillActor(context, owner);
			}
		}
	}
}

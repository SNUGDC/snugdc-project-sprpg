using System;
using System.Collections.Generic;
using System.Linq;
using Gem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SPRPG.Battle
{
	using BossDamageArgument=BossSingleArgument<Damage>;
	using BossStatusConditionArgument=BossSingleArgument<StatusConditionTest>;

	public sealed class BossPoisonPassive : BossPassive
	{
		private readonly BossStatusConditionArgument _arguments;

		public BossPoisonPassive(BossPassiveBalanceData data, Battle context, Boss owner) : base(data, context, owner)
		{
			_arguments = new BossStatusConditionArgument(data.Arguments, "StatusConditionTest");
		}

		protected override void ToggleOn()
		{
			base.ToggleOn();
			Owner.OnAfterAttack += TestAndGrantStatusConditionAfterAttack;
		}

		protected override void ToggleOff()
		{
			base.ToggleOff();
			Owner.OnAfterAttack -= TestAndGrantStatusConditionAfterAttack;
		}

		public override void ResetByStun() { }

		private void TestAndGrantStatusConditionAfterAttack(Boss owner, Pawn target)
		{
			target.TestAndGrant(_arguments.Value);
		}
	}

	public class BossRecoveryPassive : BossPassive
	{
		public BossRecoveryPassive(BossPassiveBalanceData data, Battle context, Boss owner) : base(data, context, owner)
		{
			// todo: event boss phase changed.
		}

		public override void ResetByStun() { }
	}

	public class BossRadiationAttackSkillActor : BossSingleDelayedPerformSkillActor 
	{
		private readonly BossDamageArgument _damage;

		public BossRadiationAttackSkillActor(BossSkillBalanceData data, Battle context, Boss owner)
			: base(data, context, owner, (Tick)35)
		{
			_damage = new BossDamageArgument(data.Arguments, "Damage");
		}

		protected override void Perform()
		{
			var target = Context.Party.GetAliveLeaderOrMember();
			if (target == null) return;
			if (!Owner.TestHitIfBlindAndInvokeEventIfMissed()) return;
			Owner.Attack(target, _damage);
		}
	}

	public struct BossRadiationRangeAttackArguments
	{
		public Damage Damage;
		public int[] TargetNumber;
	}

	public class BossRadiationRangeAttackSkillActor : BossSingleDelayedPerformSkillActor
	{
		private readonly BossRadiationRangeAttackArguments _arguments;

		public readonly List<OriginalPartyIdx> Targets = new List<OriginalPartyIdx>(3);

		public BossRadiationRangeAttackSkillActor(BossSkillBalanceData data, Battle context, Boss owner)
			: base(data, context, owner, (Tick)55)
		{
			_arguments = data.Arguments.ToObject<BossRadiationRangeAttackArguments>();
		}

		protected override void DoStart()
		{
			base.DoStart();

			Targets.Clear();

			var targets = Context.Party.GetAliveMembers().ToList();
			targets.Shuffle();

			var targetNumber = Random.Range(_arguments.TargetNumber[0], _arguments.TargetNumber[1] + 1);
			targetNumber = Math.Min(targetNumber, targets.Count);
			Targets.AddRange(targets.GetRange(0, targetNumber).Select(target => target.Idx));
		}

		protected override void Perform()
		{
			foreach (var target in Targets)
			{
				var character = Context.Party[target];
				if (character.IsDead) continue;
				if (!Owner.TestHitIfBlindAndInvokeEventIfMissed()) continue;
				Owner.Attack(character, _arguments.Damage);
			}
		}

		public override object MakeViewArgument()
		{
			return Targets;
		}
	}

	public class BossRadiationPoisonExplosion1 : BossSingleDelayedPerformSkillActor
	{
		private readonly List<Character> _targets = new List<Character>(3);
		private readonly BossDamageArgument _argument;

		public BossRadiationPoisonExplosion1(BossSkillBalanceData data, Battle context, Boss owner) : base(data, context, owner, (Tick)3)
		{
			_argument = new BossDamageArgument(data.Arguments, "Damage");
		}

		protected override void DoStart()
		{
			base.DoStart();
			foreach (var member in Context.Party.GetAliveMembers())
			{
				if (!member.Character.IsPoisoned) continue;
				_targets.Add(member.Character);
			}
		}

		protected override void Perform()
		{
			foreach (var target in _targets)
			{
				Owner.Attack(target, _argument.Value);
				target.TryCure(StatusConditionType.Poison);
			}
		}

		public override object MakeViewArgument()
		{
			return _targets;
		}
	}

	public class BossRadiationPoisonExplosion2 : BossSingleDelayedPerformSkillActor
	{
		public BossRadiationPoisonExplosion2(BossSkillBalanceData data, Battle context, Boss owner) : base(data, context, owner, (Tick)3)
		{
		}

		protected override void Perform()
		{
			foreach (var member in Context.Party)
			{
				if (!member.IsPoisoned) continue;
				member.SetHpForced((Hp) 1);
				member.TryCure(StatusConditionType.Poison);
			}
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
				case BossPassiveLocalKey.Poison1:
				case BossPassiveLocalKey.Poison2:
				case BossPassiveLocalKey.Poison3:
					return new BossPoisonPassive(data, context, owner);
				case BossPassiveLocalKey.Timescale1:
				case BossPassiveLocalKey.Timescale2:
					return new BossTimescalePassive(data, context, owner);
				case BossPassiveLocalKey.Recovery:
					return new BossRecoveryPassive(data, context, owner);
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
				case BossSkillLocalKey.Attack1:
				case BossSkillLocalKey.Attack2:
				case BossSkillLocalKey.Attack3:
					return new BossRadiationAttackSkillActor(data, context, owner);
				case BossSkillLocalKey.RangeAttack1:
				case BossSkillLocalKey.RangeAttack2:
				case BossSkillLocalKey.RangeAttack3:
					return new BossRadiationRangeAttackSkillActor(data, context, owner);
				case BossSkillLocalKey.PoisonExplosion1:
					return new BossRadiationPoisonExplosion1(data, context, owner);
				case BossSkillLocalKey.PoisonExplosion2:
					return new BossRadiationPoisonExplosion2(data, context, owner);
				default:
					Debug.LogError(LogMessages.EnumUndefined(data.Key));
					return new BossNoneSkillActor(context, owner);
			}
		}
	}
}

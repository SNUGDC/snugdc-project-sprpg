﻿using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public struct MonkEquilityArguments
	{
		public Damage Damage;
		public Hp Heal;

		public string Describe(string format)
		{
			format = SkillDescriptorHelper.Replace(format, "Damage", Damage.Value);
			format = SkillDescriptorHelper.Replace(format, "Heal", Heal);
			return format;
		}
	}

	public struct MonkRecoveryArguments
	{
		public Tick Duration;
		public Tick Period;
		public Hp Amount;
	}

	public struct MonkRevengeArguments
	{
		public Hp DamagePerLossHpPercentage;
	}

	public class MonkSacrificeSkillActor : SkillActor
	{
		private DamageIntercepter.Key_? _damageInterceptKey;

		public MonkSacrificeSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
		}

		protected override void DoStart()
		{
			RegisterIntercepterToParty();
			var args = Data.Arguments.ToObject<FiniteSkillArguments>();
			Context.AddBeforeTurn(args.Duration, UnregisterIntercepterToParty);
			Stop();
		}

		private void RegisterIntercepterToParty()
		{
			if (_damageInterceptKey != null)
				Debug.LogWarning("register again. continue anyway.");
			_damageInterceptKey = DamageIntercepter.IssueUniqueKey();

			foreach (var member in Context.Party)
			{
				if (member == Owner) continue;
				member.DamageIntercepter.Set(_damageInterceptKey.Value, InterceptDamage);
			}
		}

		private void UnregisterIntercepterToParty()
		{
			if (_damageInterceptKey == null)
			{
				Debug.LogError("unregister again.");
				return;
			}

			foreach (var member in Context.Party)
			{
				if (member == Owner) continue;
				member.DamageIntercepter.ClearIfKeySame(_damageInterceptKey.Value);
			}
		}

		private void InterceptDamage(Character character, Damage damage)
		{
			Owner.Hit(damage);
		}
	}

	public class MonkEquilitySkillActor : SkillActor
	{
		private readonly MonkEquilityArguments _arguments;

		public MonkEquilitySkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
			_arguments = data.Arguments.ToObject<MonkEquilityArguments>();
		}

		protected override void DoStart()
		{
			Owner.Hit(_arguments.Damage);
			foreach (var member in Context.Party)
			{
				if (Owner == member) continue;
				member.Heal(_arguments.Heal);
			}
			Stop();
		}
	}

	public class MonkRevengeSkillActor : SkillActor
	{
		private readonly MonkRevengeArguments _arguments;

		public MonkRevengeSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
			_arguments = Data.Arguments.ToObject<MonkRevengeArguments>();
		}

		protected override void DoStart()
		{
			var lossHpPercentage = Percentage._100 - Owner.HpPercentage;
			var damage = (Hp) (lossHpPercentage*(int)_arguments.DamagePerLossHpPercentage);
			Owner.Hit(new Damage(damage));
			Stop();
		}
	}

	public class MonkRecoverySkillActor : SkillActor
	{
		private readonly MonkRecoveryArguments _arguments;

		public MonkRecoverySkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
			_arguments = Data.Arguments.ToObject<MonkRecoveryArguments>();
		}

		protected override void DoStart()
		{
			for (var tick = default(Tick); tick <= _arguments.Duration; tick = tick + (int)_arguments.Period)
				Context.AddPlayerSkill(tick, () => { Owner.Heal(_arguments.Amount); });
			Stop();
		}
	}
}
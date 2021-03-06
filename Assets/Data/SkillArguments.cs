﻿using Gem;
using LitJson;
using SPRPG.Battle;

namespace SPRPG
{
	public struct FiniteSkillArguments
	{
		public readonly Tick Duration;

		public FiniteSkillArguments(JsonData args)
		{
			Duration = (Tick) args["Duration"].GetNatural();
		}
	}

	public struct AttackSkillArguments
	{
		public readonly Damage Damage;

		public AttackSkillArguments(JsonData args)
		{
			Damage = args["Damage"].ToObject<Damage>();
		}

		public string Describe(string format)
		{
			return SkillDescriptorHelper.Replace(format, "Damage", Damage.Value);
		}
	}

	public struct HealSkillArguments
	{
		public readonly Hp Amount;

		public HealSkillArguments(JsonData args)
		{
			Amount = (Hp)args["Amount"].GetNatural();
		}
	}

	public struct AttackAndGrantStatusConditionArguments
	{
		public readonly Damage Damage;
		public readonly StatusConditionTest StatusConditionTest;

		public string Describe(string format)
		{
			format = SkillDescriptorHelper.Replace(format, "Damage", Damage.Value);
			format = SkillDescriptorHelper.Replace(format, "StatusConditionPercentage", StatusConditionTest.Percentage);
			return format;
		}
	}

	public struct AttackAndTestStunArguments
	{
		public Damage Damage;
		public StunTest StunTest;

		public string Describe(string format)
		{
			format = SkillDescriptorHelper.Replace(format, "Damage", Damage.Value);
			format = SkillDescriptorHelper.Replace(format, "StunPercentage", StunTest.Percentage);
			return format;
		}
	}
}

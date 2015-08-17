using Gem;
using LitJson;

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
		public readonly Battle.StatusConditionTest StatusConditionTest;

		public string Describe(string format)
		{
			format = SkillDescriptorHelper.Replace(format, "Damage", Damage.Value);
			format = SkillDescriptorHelper.Replace(format, "StatusConditionPercentage", StatusConditionTest.Percentage);
			return format;
		}
	}

	public struct ArcherArrowRainArguments
	{
		public readonly Hp DamagePerArrow;

		public ArcherArrowRainArguments(JsonData args)
		{
			DamagePerArrow = (Hp)args["DamagePerArrow"].GetNatural();
		}
	}
}

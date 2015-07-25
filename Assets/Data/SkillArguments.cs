using Gem;
using LitJson;

namespace SPRPG
{

	public struct FiniteSkillArguments
	{
		public const string DurationKey = "Duration";

		public readonly Tick Duration;

		public FiniteSkillArguments(JsonData args)
		{
			Duration = (Tick) args[DurationKey].GetNatural();
		}
	}

	public struct AttackSkillArguments
	{
		public const string DamageKey = "Damage";

		public readonly Damage Damage;

		public AttackSkillArguments(JsonData args)
		{
			Damage = args[DamageKey].ToObject<Damage>();
		}
	}

	public struct HealSkillArguments
	{
		public const string AmountKey = "Amount";

		public readonly Hp Amount;

		public HealSkillArguments(JsonData args)
		{
			Amount = (Hp)args[AmountKey].GetNatural();
		}
	}

	public struct ArcherArrowRainArguments
	{
		public const string DamagePerArrowKey = "DamagePerArrow";

		public readonly Hp DamagePerArrow;

		public ArcherArrowRainArguments(JsonData args)
		{
			DamagePerArrow = (Hp)args[DamagePerArrowKey].GetNatural();
		}
	}
}

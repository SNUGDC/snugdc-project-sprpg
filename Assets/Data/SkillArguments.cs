using Gem;
using LitJson;

namespace SPRPG
{
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
}

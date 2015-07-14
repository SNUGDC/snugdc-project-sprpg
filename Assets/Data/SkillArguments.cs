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
}

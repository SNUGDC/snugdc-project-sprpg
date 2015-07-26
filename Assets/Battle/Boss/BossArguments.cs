using Gem;
using LitJson;

namespace SPRPG.Battle
{
	public class BossGrantStatusConditionArgument
	{
		public readonly Percentage Percentage;
		public readonly StatusConditionType StatusCondition;
	}

	public class BossDamageArgument
	{
		public readonly Damage Damage;

		public BossDamageArgument(JsonData data)
		{
			Damage = data["Damage"].ToObject<Damage>();
		}

		public static implicit operator Damage(BossDamageArgument thiz)
		{
			return thiz.Damage;
		}
	}
}

using Gem;
using LitJson;

namespace SPRPG.Battle
{
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

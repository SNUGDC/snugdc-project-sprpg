using Gem;
using LitJson;

namespace SPRPG.Battle
{
	public class BossSingleArgument<T>
	{
		public readonly T Value;

		public BossSingleArgument(JsonData data, string key)
		{
			Value = data[key].ToObject<T>();
		}

		public static implicit operator T(BossSingleArgument<T> thiz)
		{
			return thiz.Value;
		}
	}
}

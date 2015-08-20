using System;
using Gem;
using LitJson;

namespace SPRPG
{
	public static class JsonBindings
	{
		private static void BindEnumAsNatural<T>()
		{
			JsonMapper.RegisterImporter<int, T>(v => (T)(object)v);
			JsonMapper.RegisterExporter<T>((o, w) => w.Write(Convert.ToInt32(o)));
		}

		private static void BindEnumAsString<T>()
		{
			JsonMapper.RegisterImporter<string, T>(v =>
			{
				T ret;
				EnumHelper.TryParse(v, out ret);
				return ret;
			});

			JsonMapper.RegisterExporter<T>((o, w) => w.Write(o.ToString()));
		}

		public static void Setup()
		{
			UnityTypeBindings.Register();
			BindEnumAsNatural<Percentage>();
			BindEnumAsNatural<Weight>();
			BindEnumAsNatural<SkillTier>();
			BindEnumAsNatural<Hp>();
			BindEnumAsNatural<Tick>();
			BindEnumAsString<Element>();
			BindEnumAsString<StageId>();
			BindEnumAsString<CharacterId>();
			BindEnumAsString<BossId>();
			BindEnumAsString<PassiveKey>();
			BindEnumAsString<SkillKey>();
			BindEnumAsString<StatusConditionType>();
			BindEnumAsString<Battle.ConditionType>();
			BindEnumAsString<Battle.ActTarget>();
			BindEnumAsString<Battle.ActAction>();
			BindEnumAsString<Battle.BossPassiveLocalKey>();
			BindEnumAsString<Battle.BossSkillLocalKey>();

			BindStringOrDictionaryText();
			BindCondition();
		}

		private static void BindStringOrDictionaryText()
		{
			JsonMapper.RegisterImporter<string, StringOrDictionaryText>(v => new StringOrDictionaryText(false, v));
			JsonMapper.RegisterImporter<JsonData, StringOrDictionaryText>(v => new StringOrDictionaryText(true, (string)v["Key"]));
		}

		private static void BindCondition()
		{
			JsonMapper.RegisterImporter<JsonData, Battle.Condition>(Battle.ConditionFactory.Create);
		}
	}
}

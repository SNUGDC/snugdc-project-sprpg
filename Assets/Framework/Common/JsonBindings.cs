using System;
using Gem;
using LitJson;

namespace SPRPG
{
	public static class JsonBindings
	{
		public static void BindEnumAsNatural<T>()
		{
			JsonMapper.RegisterImporter<int, T>(v => (T)(object)v);
			JsonMapper.RegisterExporter<T>((o, w) => w.Write(Convert.ToInt32(o)));
		}

		public static void BindEnumAsString<T>()
		{
			JsonMapper.RegisterImporter<string, T>(v => (T) Enum.Parse(typeof(T), v));
			JsonMapper.RegisterExporter<T>((o, w) => w.Write(o.ToString()));
		}

		public static void Setup()
		{
			UnityTypeBindings.Register();
			BindEnumAsString<Element>();
			BindEnumAsString<CharacterId>();
			BindEnumAsString<SkillKey>();
			BindEnumAsNatural<SkillTear>();
			BindEnumAsNatural<Battle.Hp>();
		}
	}
}

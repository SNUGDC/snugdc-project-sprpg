﻿using System;
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
			BindEnumAsNatural<SkillTear>();
			BindEnumAsNatural<Battle.Tick>();
			BindEnumAsNatural<Battle.Proportion>();
			BindEnumAsNatural<Battle.Hp>();
			BindEnumAsString<Element>();
			BindEnumAsString<CharacterId>();
			BindEnumAsString<BossId>();
			BindEnumAsString<SkillKey>();
			BindEnumAsString<Battle.BossConditionType>();
			BindEnumAsString<Battle.BossSkillLocalKey>();
		}
	}
}

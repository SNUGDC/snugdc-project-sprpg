#if BALANCE
using Gem;
#endif
using SPRPG.Battle;

namespace SPRPG
{
	public static class DebugConfig
	{
		public struct Battle_
		{
			public struct Log_
			{
				public bool Input;
				public bool CharacterHpChanged;
				public bool CharacterDead;
				public bool BossSkillStart;

				public static Log_ Default()
				{
					return new Log_
					{
						Input = false,
						CharacterHpChanged = false,
						CharacterDead = false,
						BossSkillStart = false,
					};
				}
			}

			public struct Boss_
			{
				public BossSkillLocalKey? UseOnlySkill;

				public static Boss_ Default()
				{
					return new Boss_
					{
						UseOnlySkill = null,
					};
				}
			}

			public Log_ Log;
			public Boss_ Boss;

			public static Battle_ Default()
			{
				return new Battle_
				{
					Log = Log_.Default(),
					Boss = Boss_.Default(),
				};
			}
		}

		private struct Data
		{
			public bool Draw;
			public bool UseDefaultConfig;
			public bool ReloadData;
			public Battle_ Battle;
		}


#if BALANCE
		private static Data _data;

		static DebugConfig()
		{
			Load();
		}
#else
		private static readonly Data _data;

		static DebugConfig()
		{
			_data.Draw = false;
			_data.UseDefaultConfig = false;
			_data.ReloadData = false;
			_data.Battle = Battle_.Default();
		}
#endif

		public static void Load()
		{
#if BALANCE
			Data tmp;
			if (JsonHelper.LoadFromResources("Data/Data/debug", out tmp))
				_data = tmp;
#endif
		}

		public static bool Draw { get { return _data.Draw; } }
		public static bool UseDefaultConfig { get { return _data.UseDefaultConfig; } }
		public static bool ReloadData { get { return _data.ReloadData; } }
		public static Battle_ Battle { get { return _data.Battle; } }
	}
}
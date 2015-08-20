namespace SPRPG
{
	public static class DataManager
	{
		public static void Load()
		{
			DebugConfig.Load();
			Config.TryLoad();
			TextDictionary.Load();
			CampBalance._.Load();
			CharacterBalance._.Load();
			Battle.ConditionDictionary._.Load();
			SkillBalance._.Load();
			Battle.BossBalance._.Load();
			Battle.BattleBalance._.Load();
		}

#if BALANCE
		public static void Reload()
		{
			DebugConfig.Load();
			Config.LoadForced();
			TextDictionary.Load();
			CampBalance._.LoadForced();
			CharacterBalance._.LoadForced();
			Battle.ConditionDictionary._.LoadForced();
			SkillBalance._.LoadForced();
			Battle.BossBalance._.Load();
			Battle.BattleBalance._.LoadForced();
			CharacterDb._.Build();
		}
#endif
	}
}

﻿using SPRPG.Battle;

namespace SPRPG
{
	public static class DataManager
	{
#if BALANCE
		public static void Reload()
		{
			DebugConfig.Load();
			Config.LoadForced();
			CampBalance._.LoadForced();
			BattleBalance._.LoadForced();
		}
#endif
	}
}

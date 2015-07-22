using UnityEngine;

namespace SPRPG
{
	public static class Transition
	{
		private const string CampSceneName = "Camp";
		private const string WorldSceneName = "World";
		private const string BattleSceneName = "Battle";
		private const string ProfileSceneName = "Profile";
		private const string SettingSceneName = "Setting";

		public static void TransferToCamp()
		{
			Application.LoadLevel(CampSceneName);
		}

		public static void TransferToWorld()
		{
			Application.LoadLevel(WorldSceneName);
		}

		public static void TransferToBattleWithUserBattleDef(StageId stage)
		{
			TransferToBattle(UserUtil.MakeBattleDef(stage));
		}

		public static void TransferToBattleWithoutDef()
		{
			Debug.LogWarning("trying to transfer without def. sure?");
			Application.LoadLevel(BattleSceneName);
		}

		public static void TransferToBattle(Battle.BattleDef def)
		{
			Battle.BattleWrapper.Def = def;
			Application.LoadLevel(BattleSceneName);
		}

		public static void TransferToProfile()
		{
			Application.LoadLevel(ProfileSceneName);
		}
		
		public static void TransferToSetting()
		{
			Application.LoadLevel(SettingSceneName);
		}
	}
}
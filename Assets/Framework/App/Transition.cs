using UnityEngine;

namespace SPRPG
{
	public static class Transition
	{
		private const string CampSceneName = "Camp";
		private const string WorldSceneName = "World";
		private const string StageSceneName = "Stage";
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

		public static void TransferToStage()
		{
			Application.LoadLevel(StageSceneName);
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
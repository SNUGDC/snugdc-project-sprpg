using UnityEngine;

namespace SPRPG
{
	public static class Transition
	{
		private const string CampSceneName = "Camp";
		private const string WorldSceneName = "World";
		private const string StageSceneName = "Stage";

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
	}
}
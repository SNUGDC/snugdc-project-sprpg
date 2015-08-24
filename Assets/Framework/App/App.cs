#pragma warning disable 0168

using Gem;

namespace SPRPG
{
	public class App : Singleton<App>
	{
		public static void Init()
		{
			var awake = _;
		}

		void Update()
		{
			TheGem.Update();
		}

#if UNITY_EDITOR
		void OnLevelWasLoaded()
		{
			if (DebugConfig.ReloadData)
				DataManager.Reload();
		}
#endif
	}
}
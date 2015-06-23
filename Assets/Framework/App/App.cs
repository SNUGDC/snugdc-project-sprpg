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

#if BALANCE
		void OnLevelWasLoaded()
		{
			if (DebugConfig.ReloadData)
				DataManager.Reload();
		}
#endif
	}
}
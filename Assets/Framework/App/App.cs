#pragma warning disable 0168

namespace SPRPG
{
	public class App : Singleton<App>
	{
		public static void Init()
		{
			var awake = _;
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
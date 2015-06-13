namespace SPRPG
{
	public class App : Singleton<App>
	{
		public static void Init() {}

#if BALANCE
		void OnLevelWasLoaded()
		{
			if (DebugConfig.ReloadData)
				DataManager.Reload();
		}
#endif
	}
}
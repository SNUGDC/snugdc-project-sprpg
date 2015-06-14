#if BALANCE
using Gem;
#endif

namespace SPRPG
{
	public static class DebugConfig
	{
		private struct Data
		{
			public bool Draw;
			public bool UseDefaultConfig;
			public bool ReloadData;
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
		}
#endif

		public static void Load()
		{
#if BALANCE
			Data tmp;
			if (JsonHelper.Load("Raw/Data/debug.json", out tmp))
				_data = tmp;
#endif
		}

		public static bool Draw { get { return _data.Draw; } }
		public static bool UseDefaultConfig { get { return _data.UseDefaultConfig; } }
		public static bool ReloadData { get { return _data.ReloadData; } }
	}
}
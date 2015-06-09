﻿using JetBrains.Annotations;

#if UNITY_EDITOR
using Gem;
#endif

namespace SPRPG
{
	public static class DebugConfig
	{
		private struct Data
		{
			[UsedImplicitly] public bool UseDefaultConfig;
		}

		private static readonly Data _data;

#if UNITY_EDITOR
		static DebugConfig()
		{
			JsonHelper.Load("Raw/Data/debug.json", out _data);
		}
#else
		static DebugConfig()
		{
			_data.UseDefaultConfig = false;
		}
#endif

		public static bool UseDefaultConfig { get { return _data.UseDefaultConfig; } }
	}
}
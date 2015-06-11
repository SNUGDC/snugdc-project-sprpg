using System.IO;
using Gem;
using UnityEngine;

namespace SPRPG
{
	public static class Config
	{
		private const string _defaultPath = "Data/default_config";
		private const string _filename = "config.json";

		private static ConfigData _data;

		public static ConfigData Data
		{
			get
			{
				if (_data == null)
					TryLoad();
				return _data;
			}
		}

#if UNITY_EDITOR
		public static string GetDefaultPath()
		{
			return Directory.GetCurrentDirectory() + "/Assets/Resources/" + _defaultPath + ".json";
		}
#endif

		public static string GetSavePath()
		{
			return Application.persistentDataPath + "/" + _filename;
		}

		public static void TryLoad()
		{
			if (_data == null)
				LoadForced();
		}

		private static ConfigData Load()
		{
			ConfigData ret;

			if (!DebugConfig.UseDefaultConfig 
				&& JsonHelper.Load(GetSavePath(), out ret))
				return ret;

			if (JsonHelper.LoadFromResources(_defaultPath, out ret))
				return ret;

			return null;
		}

		public static void LoadForced()
		{
			_data = Load();
		}

		public static bool Save()
		{
			if (_data == null)
			{
				Debug.LogError("save before load.");
				return false;
			}

			return JsonHelper.Save(GetSavePath(), _data);
		}
	}
}

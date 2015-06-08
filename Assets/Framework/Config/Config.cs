using Gem;
using LitJson;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SPRPG
{
	public static class Config
	{
		private const string _defaultPath = "Data/default_config";
		private const string _filename = "config.json";

		private static JsonData _data;

		public static JsonData Data
		{
			get { return _data ?? (_data = Load()); }
		}

#if UNITY_EDITOR
		public static string GetDefaultPath()
		{
			return "Assets/Resources/" + _defaultPath;
		}
#endif

		public static string GetSavePath()
		{
			return Application.persistentDataPath + "/" + _filename;
		}

		private static JsonData Load()
		{
			JsonData ret;
			if (JsonHelper.Load(GetSavePath(), out ret))
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

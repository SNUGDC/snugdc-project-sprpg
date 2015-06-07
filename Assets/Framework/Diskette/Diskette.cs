using System.IO;
using Gem;
using LitJson;
using UnityEngine;

namespace SPRPG
{
	public static partial class Diskette
	{
		private const string DefaultSaveFilePath = "Data/default_save_file";

		public static bool IsLoaded { get; private set; }
		public static string SaveName { get; private set; }

		public static string GetSaveDirectory()
		{
			return Application.persistentDataPath + "/Save/";
		}

		public static string GetSavePath(string name)
		{
			return GetSaveDirectory() + name + ".json";
		}

		private static SaveData LoadDefault()
		{
			var text = Resources.Load<TextAsset>(DefaultSaveFilePath);
			System.Diagnostics.Debug.Assert(text != null);
			using (TextReader reader = new StringReader(text.text))
				return JsonMapper.ToObject<SaveData>(reader);
		}

		public static bool Load(string name)
		{
			if (IsLoaded)
			{
				Debug.LogError("trying to load again.");
				return true;
			}

			return LoadForced(name);
		}

		public static bool LoadForced(string name)
		{
			SaveData saveData;

			var path = GetSavePath(name);

			if (!File.Exists(path))
			{
				saveData = LoadDefault();
			}
			else
			{
				if (!JsonHelper.Load(path, out saveData))
				{
					Debug.LogError("load failed " + path + ". load default instead.");
					saveData = LoadDefault();
				}
			}

			if (!DoLoad(saveData))
				return false;

			SaveName = name;
			IsLoaded = true;

			return true;
		}

		public static bool Save()
		{
			if (!IsLoaded)
			{
				Debug.LogError("save before load.");
				return false;
			}

			var saveDir = GetSaveDirectory();
			if (!Directory.Exists(saveDir))
				Directory.CreateDirectory(saveDir);

			var saveData = DoSave();

			if (saveData == null)
				return false;

			var path = GetSavePath(SaveName);
			if (!JsonHelper.Save(path, saveData))
				return false;

			return true;
		}
	}
}
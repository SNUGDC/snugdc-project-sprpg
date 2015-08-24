using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG
{
	public static class TextDictionary
	{
		private static Dictionary<string, string> _dict;

		static TextDictionary()
		{
			Load();
		}

		public static bool Load()
		{
			var success = JsonHelper.LoadFromResources("Data/Data/string", out _dict);
			if (!success)
			{
				Debug.LogError("load string.json failed.");
				_dict = new Dictionary<string, string>();
			}
			return success;
		}

		public static string Get(string key)
		{
			string ret;
			if (!_dict.TryGet(key, out ret))
				ret = "[key not found: " + key + "]";
			return ret;
		}
	}
}
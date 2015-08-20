using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;
using UnityEngine;

namespace Gem
{
	public static class JsonHelper
	{
		private static string MakeMessageWrongType(JsonType expected, JsonType matches)
		{
			return "JsonData has wrong type.\n"
			       + "expected: " + expected + ", matches: " + matches;
		}

		public static IEnumerable<JsonData> GetListEnum(this JsonData data)
		{
			return data.Cast<JsonData>();
		}

		public static IEnumerable<KeyValuePair<string, JsonData>> GetDictEnum(this JsonData data)
		{
			return data.Cast<KeyValuePair<string, JsonData>>();
		}

		public static bool TryGet(this JsonData data, string key, out JsonData val)
		{
			if (!data.Keys.Contains(key))
			{
				Debug.LogWarning(LogMessages.KeyNotExists(key));
				val = null;
				return false;
			}

			val = data[key];
			return true;
		}

		public static bool TryGet(this JsonData data, string key, out string val)
		{
			JsonData ret;
			
			if (data.TryGet(key, out ret))
			{
				if (ret.IsString)
				{
					val = (string) ret;
					return true;
				}
				else
				{
					Debug.LogError(MakeMessageWrongType(JsonType.String, ret.GetJsonType()));
				}
			}

			val = string.Empty;
			return false;
		}

		public static bool TryGet(this JsonData thiz, string key, out bool val)
		{
			JsonData data;
			if (thiz.TryGet(key, out data))
			{
				if (data.IsBoolean)
				{
					val = (bool) data;
				}
				else
				{
					Debug.LogError(MakeMessageWrongType(JsonType.Boolean, data.GetJsonType()));
				}
			}

			val = default(bool);
			return false;
		}

		public static bool TryGet(this JsonData thiz, string key, out int val)
		{
			JsonData data;
			
			if (thiz.TryGet(key, out data))
			{
				if (data.IsNatural)
				{
					val = (int) data;
					return true;
				}
				else
				{
					Debug.LogError(MakeMessageWrongType(JsonType.Natural, data.GetJsonType()));
				}
			}

			val = default(int);
			return false;
		}
		
		public static bool TryGet(this JsonData thiz, string key, out float val)
		{
			JsonData data;
			
			if (thiz.TryGet(key, out data))
			{
				if (data.IsNatural)
				{
					val = (int) data;
					return true;
				}
				else if (data.IsReal)
				{
					val = (float) data;
					return true;
				}
				else
				{
					Debug.LogError(MakeMessageWrongType(JsonType.Real, data.GetJsonType()));
				}
			}

			val = default(float);
			return false;
		}

		public static bool BoolOrDefault(this JsonData thiz, string key, bool _default = false)
		{
			bool ret;
			return thiz.TryGet(key, out ret) ? ret : _default;
		}

		public static int IntOrDefault(this JsonData thiz, string key, int _default = 0)
		{
			int ret;
			return thiz.TryGet(key, out ret) ? ret : _default;
		}

		public static float FloatOrDefault(this JsonData thiz, string key, float _default = 0)
		{
			float ret;
			return thiz.TryGet(key, out ret) ? ret : _default;
		}

		public static string StringOrDefault(this JsonData thiz, string key, string _default = "")
		{
			string ret;
			return thiz.TryGet(key, out ret) ? ret : _default;
		}

		public static T ParseOrDefault<T>(this JsonData thiz)
		{
			Debug.Assert(thiz.IsString);
			return EnumHelper.ParseOrDefault<T>((string) thiz);
		}

		public static T ToObject<T>(this JsonData thiz)
		{
			return JsonMapper.ToObject<T>(JsonMapper.ToJson(thiz));
		}

		public static T ToEnum<T>(this JsonData thiz)
		{
			return EnumHelper.ParseOrDefault<T>((string) thiz);
		}

		public static bool Load<T>(string path, out T data) 
		{
			try
			{
				using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
				using (var reader = new StreamReader(fs))
				{
					data = JsonMapper.ToObject<T>(new JsonReader(reader));
					return true;
				}
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				data = default(T);
				return false;
			}
		}

		public static bool LoadFromResources<T>(string path, out T data) 
		{
			var text = Resources.Load<TextAsset>(path);
			if (text == null)
			{
				Debug.LogWarning("text asset on " + path + " does not exists.");
				data = default(T);
				return false;
			}
			
			using (TextReader reader = new StringReader(text.text))
				data = JsonMapper.ToObject<T>(reader);

			return true;
		}

		public static bool Save<T>(string path, T data)
		{
			try
			{
				using (var fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
				using (var writer = new StreamWriter(fs))
				{
					JsonMapper.ToJson(data, new JsonWriter(writer));
					return true;
				}
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				return false;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Gem
{
	public static class EnumHelper
	{
		[Conditional("DEBUG")]
		public static void AssertEnum<T>()
		{
			System.Diagnostics.Debug.Assert(typeof(T).IsEnum);
		}

		public static bool IsDefault(this Enum _enum)
		{
			return Convert.ToUInt64(_enum) == 0;
		}

		public static bool Has<T>(this Enum _enum, T val)
		{
			AssertEnum<T>();
			var valInt = Convert.ToUInt64(val);
			return (Convert.ToUInt64(_enum) & valInt) == valInt;
		}

		public static bool No<T>(this Enum _enum, T val)
		{
			return !Has(_enum, val);
		}

		public static IEnumerable<T> GetValues<T>()
		{
			AssertEnum<T>();
			return (IEnumerable<T>) Enum.GetValues(typeof(T));
		}
		
		public static bool IsSingular<T>(T _enum)
		{
			AssertEnum<T>();
			return Enum.IsDefined(typeof(T), _enum);
		}

		public static bool TryParse<T>(string str, out T ret)
		{
			AssertEnum<T>();

			if (Enum.IsDefined(typeof(T), str))
			{
				ret = (T)Enum.Parse(typeof(T), str, true);
				return true;
			}

			ret = default(T);
			Debug.Log(LogMessages.ParseFailed<T>(str));
			return false;
		}

		public static T ParseOrDefault<T>(string str)
		{
			T ret;
			return TryParse(str, out ret) ? ret : default(T);
		}

		public static bool TryParseAsInt<T>(string str, out T ret)
		{
			AssertEnum<T>();

			int intVal;

			if (!Int32.TryParse(str, out intVal))
			{
				ret = default(T);
				return false;
			}

			ret = (T)(object)intVal;
			return true;
		}

		public static T ParseAsIntOrDefault<T>(string str)
		{
			T ret;
			return TryParseAsInt(str, out ret) ? ret : default(T);
		}
	}
}
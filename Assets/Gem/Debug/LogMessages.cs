namespace Gem
{
	public static class LogMessages
	{
		public static string ParseFailed<T>(string str)
		{
			return "parse " + str + " to enum " + typeof(T).Name + " undefined.";
		}

		public static string EnumUndefined<T>(T val)
		{
			return "enum " + val + " of " + typeof(T).Name + " undefined.";
		}

		public static string KeyExists<T>(T key)
		{
			return "key " + key + " exists.";
		}

		public static string KeyNotExists<T>(T key)
		{
			return "key " + key + " not exists.";
		}
	}
}

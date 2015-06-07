namespace Gem
{
	public static class LogMessages
	{
		public static string EnumUndefined<T>(string str)
		{
			return "enum " + str + " of " + typeof(T).Name + " undefined.";
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

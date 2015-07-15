using System;

namespace Gem
{
	public static class CSharpHelper
	{
		public static void CheckAndCall(this Action act)
		{
			if (act != null) act();
		}

		public static void CheckAndCall<T1>(this Action<T1> act, T1 p1)
		{
			if (act != null) act(p1);
		}

		public static void CheckAndCall<T1, T2>(this Action<T1, T2> act, T1 p1, T2 p2)
		{
			if (act != null) act(p1, p2);
		}

		public static void CheckAndCall<T1, T2, T3>(this Action<T1, T2, T3> act, T1 p1, T2 p2, T3 p3)
		{
			if (act != null) act(p1, p2, p3);
		}
	}
}
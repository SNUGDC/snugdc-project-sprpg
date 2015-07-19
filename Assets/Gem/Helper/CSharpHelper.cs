using System;
using System.Collections.Generic;
using System.Linq;

namespace Gem
{
	public static class CSharpHelper
	{
		public static int ModPositive(int x, int y) { return (x%y + y)%y; }

		public static IEnumerable<Pair<int, int>> Range2D(int len1, int len2)
		{
			foreach (var i1 in Enumerable.Range(0, len1))
			{
				foreach (var i2 in Enumerable.Range(0, len2))
				{
					yield return new Pair<int, int>(i1, i2);
				}
			}
		}

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
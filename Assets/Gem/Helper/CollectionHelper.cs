using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Gem
{

	public enum PositionType
	{
		FRONT,
		BACK,
	}

	public static class CollectionHelper
	{
		public static bool Empty<T>(this T c) where T : ICollection
		{
			return c.Count == 0;
		}

		public static IEnumerable<T> GetReverseEnum<T>(this T[] c)
		{
			for (var i = c.Length - 1; i >= 0; i--)
				yield return c[i];
		}

		public static T GetOrDefault<T>(this List<T> c, int i)
		{
			return i < c.Count ? c[i] : default(T);
		}

		public static bool RemoveBack<T>(this List<T> c)
		{
			if (c.Count == 0)
				return false;
			c.RemoveAt(c.Count - 1);
			return true;
		}

		public static bool RemoveIf<T>(this List<T> c, Predicate<T> pred)
		{
			var i = 0;

			foreach (var _data in c)
			{
				if (pred(_data))
				{
					c.RemoveAt(i);
					return true;
				}

				i++;
			}

			return false;
		}

		public static T FindAndRemoveIf<T>(this List<T> c, Predicate<T> pred) 
		{
			var i = 0;

			foreach (var _data in c)
			{
				if (pred(_data))
				{
					c.RemoveAt(i);
					return _data;
				}

				++i;
			}

			return default(T);
		}

		public static void Resize<T>(this List<T> c, int size, T init = default(T))
		{
			var curSize = c.Count;

			if (size < curSize)
			{
				c.RemoveRange(size, curSize - size);
			}
			else if (size > curSize)
			{
				if (size > c.Capacity)
					c.Capacity = size;
				c.AddRange(Enumerable.Repeat(init, size - curSize));
			}
		}

		public static IEnumerable<T> GetReverseEnum<T>(this List<T> c)
		{
			for (var i = c.Count - 1; i >= 0; i--)
				yield return c[i];
		}

		public static int BinarySearch<T>(this IList<T> c, Func<T, int> cmp)
		{
			var min = 0;
			var max = c.Count - 1;

			while (min <= max)
			{
				var mid = (min + max) / 2;
				var cmpVal = cmp(c[mid]);

				if (cmpVal == 0)
					return mid;

				if (cmpVal < 0)
					min = mid + 1;
				else
					max = mid - 1;
			}

			return ~min;
		}

		public static T Rand<T>(this IList<T> c)
		{
			System.Diagnostics.Debug.Assert(c.Count != 0);
			return c[UnityEngine.Random.Range(0, c.Count)];
		}

		public static void Shuffle<T>(this IList<T> c)
		{
			var rng = new Random();
			var n = c.Count;
			while (n > 1)
			{
				--n;
				var k = rng.Next(n + 1);
				var value = c[k];
				c[k] = c[n];
				c[n] = value;
			}
		}

		public static void Add<T>(this LinkedList<T> c, T val, PositionType position)
		{
			if (position == PositionType.FRONT)
				c.AddFirst(val);
			else
				c.AddLast(val);
		}

		public static bool RemoveIf<T>(this LinkedList<T> c, Predicate<T> pred)
		{
			for (var node = c.First; node != null; node = node.Next)
			{
				if (pred(node.Value))
				{
					c.Remove(node);
					return true;
				}
			}

			return false;
		}

		public static bool TryGet<K, V>(this IDictionary<K, V> c, K key, out V val)
		{
			if (!c.TryGetValue(key, out val))
			{
				Debug.LogWarning("key " + key + " not exists.");
				return false;
			}
			return true;
		}

		public static bool TryGetAndParse<K, T>(this IDictionary<K, string> c, K key, out T val)
		{
			string valStr;

			if (!c.TryGet(key, out valStr))
			{
				val = default(T);
				return false;
			}

			return EnumHelper.TryParse(valStr, out val);
		}

		public static bool TryGetAndParse<K>(this IDictionary<K, string> c, K key, out int val)
		{
			string valStr;

			if (!c.TryGet(key, out valStr))
			{
				val = 0;
				return false;
			}

			return int.TryParse(valStr, out val);
		}

		public static V GetOrDefault<K, V>(this IDictionary<K, V> c, K key, V _default = default(V))
		{
			V val;
			return c.TryGetValue(key, out val) ? val : _default;
		}

		public static V GetOrPut<K, V>(this IDictionary<K, V> c, K key) where V : class, new()
		{
			var val = c.GetOrDefault(key);
			if (val != null) 
				return val;

			val = new V();
			c[key] = val;
			return val;
		}

		public static bool TryAdd<K, V>(this IDictionary<K, V> c, K key, V val)
		{
			try
			{
				c.Add(key, val);
				return true;
			}
			catch (Exception)
			{
				LogMessages.KeyExists(key);
				return false;
			}
		}

		public static bool TryRemove<K, V>(this IDictionary<K, V> c, K key)
		{
			var _ret = c.Remove(key);
			if (!_ret) LogMessages.KeyNotExists(key);
			return _ret;
		}

		public static bool GetAndRemove<K, V>(this IDictionary<K, V> c, K key, out V val)
		{
			if (c.TryGetValue(key, out val))
			{
				c.Remove(key);
				return true;
			}
			else
			{
				Debug.LogError(LogMessages.KeyNotExists(key));
				return false;
			}
		}

		public static void RemoveIf<K, V>(this IDictionary<K, V> c, Func<K, V, bool> pred)
		{
			var remove = new List<K>();

			foreach (var kv in c)
			{
				if (pred(kv.Key, kv.Value))
					remove.Add(kv.Key);
			}

			foreach (var key in remove)
			{
				c.Remove(key);
			}
		}

		public static bool Empty<T>(this HashSet<T> c)
		{
			return c.Count == 0;
		}

		public static bool TryAdd<T>(this HashSet<T> c, T val)
		{
			if (!c.Add(val))
			{
				Debug.LogError(LogMessages.KeyExists(val));
				return false;
			}

			return true;
		}

		public static bool TryRemove<T>(this HashSet<T> c, T val)
		{
			if (!c.Remove(val))
			{
				Debug.LogError(LogMessages.KeyNotExists(val));
				return false;
			}

			return true;
		}
	}
}
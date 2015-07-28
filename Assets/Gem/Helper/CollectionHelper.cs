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

		public static int? SetFirstIf<T>(this T[] c, T value, Predicate<T> pred) where T : class
		{
			var i = 0;

			foreach (var data in c)
			{
				if (pred(data))
				{
					c[i] = value;
					return i;
				}

				++i;
			}

			return null;
		}

		public static T GetOrDefault<T>(this List<T> c, int i)
		{
			return i < c.Count ? c[i] : default(T);
		}

		public static bool RemoveBack<T>(this List<T> c) 
		{
			if (c.Count == 0)
			{
				Debug.LogError("empty.");
				return false;
			}

			c.RemoveAt(c.Count - 1);
			return true;
		}

		public static bool PopBack<T>(this List<T> c, out T val) 
		{
			if (c.Count == 0)
			{
				Debug.LogError("empty.");
				val = default(T);
				return false;
			}

			val = c[c.Count - 1];
			c.RemoveAt(c.Count - 1);
			return true;
		}


		public static bool RemoveIf<T>(this IList<T> c, Predicate<T> pred)
		{
			var i = 0;

			foreach (var data in c)
			{
				if (pred(data))
				{
					c.RemoveAt(i);
					return true;
				}

				++i;
			}

			return false;
		}

		public static T FindAndRemoveIf<T>(this IList<T> c, Predicate<T> pred) 
		{
			var i = 0;

			foreach (var data in c)
			{
				if (pred(data))
				{
					c.RemoveAt(i);
					return data;
				}

				++i;
			}

			return default(T);
		}

		public static int? SetFirstIf<T>(this List<T> c, T value, Predicate<T> pred) where T : class
		{
			var i = 0;

			foreach (var data in c)
			{
				if (pred(data))
				{
					c[i] = value;
					return i;
				}

				++i;
			}

			return null;
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

		public static Dictionary<K, V> Map<O, K, V>(this List<O> c, Func<O, KeyValuePair<K, V>> mapper)
		{
			var ret = new Dictionary<K, V>(c.Count);
			foreach (var val in c)
				ret.Add(mapper(val));
			return ret;
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

		public static bool TryPopLast<T>(this LinkedList<T> c, out T val)
		{
			if (c.Empty())
			{
				val = default(T);
				return false;
			}

			val = c.Last.Value;
			c.RemoveLast();
			return true;
		}

		public static T PopLast<T>(this LinkedList<T> c)
		{
			T ret;
			if (!c.TryPopLast(out ret))
				Debug.LogError("empty.");
			return ret;
		}

		public static T PopOrDefault<T>(this LinkedList<T> c, T defaultValue)
		{
			T ret;
			if (!c.TryPopLast(out ret))
				return defaultValue;	
			return ret;
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

		public static void Add<K, V>(this IDictionary<K, V> c, KeyValuePair<K, V> kv)
		{
			c.Add(kv.Key, kv.Value);
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

		public static bool TryGetAndRemove<K, V>(this IDictionary<K, V> c, K key, out V val)
		{
			if (c.TryGetValue(key, out val))
			{
				c.Remove(key);
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool GetAndRemove<K, V>(this IDictionary<K, V> c, K key, out V val)
		{
			if (!c.TryGetAndRemove(key, out val))
			{
				Debug.LogError(LogMessages.KeyNotExists(key));
				return false;
			}

			return true;
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

		public static Dictionary<K2, V2> Map<K1, V1, K2, V2>(this IDictionary<K1, V1> c, Func<K1, V1, KeyValuePair<K2, V2>> mapper)
		{
			var ret = new Dictionary<K2, V2>(c.Count);
			foreach (var kv in c)
				ret.Add(mapper(kv.Key, kv.Value));
			return ret;
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
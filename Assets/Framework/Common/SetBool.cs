using System.Collections.Generic;
using Gem;

namespace SPRPG
{
	public class SetBool<T>
	{
		private readonly HashSet<T> _set = new HashSet<T>();

		public bool Contains(T value) { return _set.Contains(value); }
		public bool Test() { return !_set.Empty(); }

		public bool Add(T value)
		{
			_set.Add(value);
			return _set.Count == 1;
		}

		public bool Remove(T value)
		{
			_set.Remove(value);
			return _set.Empty();
		}

		public static implicit operator bool(SetBool<T> thiz) { return thiz.Test(); }
	}
}

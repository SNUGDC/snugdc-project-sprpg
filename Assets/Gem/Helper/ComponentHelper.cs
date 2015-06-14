using UnityEngine;

namespace Gem
{
	public static class ComponentHelper
	{
		public static GameObject Instantiate(this GameObject _this)
		{
			return Object.Instantiate(_this);
		}

		public static T Instantiate<T>(this T _this) where T : Component
		{
			return _this.gameObject.Instantiate().GetComponent<T>();
		}

		public static T AddComponent<T>(this Component _this) where T : Component
		{
			return _this.gameObject.AddComponent<T>();
		}

		public static T AddIfNotExists<T>(this GameObject _this) where T : Component
		{
			var ret = _this.GetComponent<T>();
			return ret ?? _this.AddComponent<T>();
		}
	}
}
using UnityEngine;

namespace Gem
{
	public static class ComponentHelper
	{
		public static GameObject Instantiate(this GameObject thiz)
		{
			return Object.Instantiate(thiz);
		}

		public static GameObject InstantiateToParentAndToggleActive(this GameObject thiz, Transform parent)
		{
			var go = thiz.Instantiate();
			go.transform.SetParent(parent, false);

			// prevent mysterious initial particle curve.
			var activeOrg = go.activeSelf;
			if (activeOrg)
			{
				go.SetActive(false);
				go.SetActive(true);
			}

			return go;
		}

		public static T Instantiate<T>(this T thiz) where T : Component
		{
			return thiz.gameObject.Instantiate().GetComponent<T>();
		}

		public static T AddComponent<T>(this Component thiz) where T : Component
		{
			return thiz.gameObject.AddComponent<T>();
		}

		public static T AddIfNotExists<T>(this GameObject thiz) where T : Component
		{
			var ret = thiz.GetComponent<T>();
			return ret ?? thiz.AddComponent<T>();
		}
	}
}
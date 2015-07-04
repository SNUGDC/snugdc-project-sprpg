using UnityEngine;

namespace Gem
{
	public static class UnityHelper
	{
		public static Vector2 ScaleInverse(this Vector2 thiz, Vector2 val)
		{
			return new Vector2(thiz.x / val.x, thiz.y / val.y);
		}

		public static RectTransform GetRectTransform(this GameObject thiz)
		{
			return (RectTransform) thiz.transform;
		}

		public static RectTransform GetRectTransform<T>(this T thiz) where T : Component
		{
			return (RectTransform) thiz.transform;
		}

		public static void SetAnchor(this RectTransform thiz, Vector2 val)
		{
			thiz.anchorMax = val;
			thiz.anchorMin = val;
		}
	}
}

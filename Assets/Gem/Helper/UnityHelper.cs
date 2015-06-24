﻿using UnityEngine;

namespace Gem
{
	public static class UnityHelper
	{
		public static Vector2 ScaleInverse(this Vector2 _this, Vector2 val)
		{
			return new Vector2(_this.x / val.x, _this.y / val.y);
		}

		public static RectTransform GetRectTransform(this GameObject _this)
		{
			return (RectTransform) _this.transform;
		}

		public static RectTransform GetRectTransform<T>(this T _this) where T : Component
		{
			return (RectTransform) _this.transform;
		}

		public static void SetAnchor(this RectTransform _this, Vector2 val)
		{
			_this.anchorMax = val;
			_this.anchorMin = val;
		}

		public static void TranslateAnchor(this RectTransform _this, Vector2 val)
		{
			var parentRect = _this.transform.parent.GetRectTransform();
			var parentSize = parentRect.offsetMax - parentRect.offsetMin;
			var deltaAnchor = val.ScaleInverse(parentSize);

			_this.anchorMin += deltaAnchor;
			_this.anchorMax += deltaAnchor;
			_this.anchoredPosition = Vector2.zero;
		}
	}
}
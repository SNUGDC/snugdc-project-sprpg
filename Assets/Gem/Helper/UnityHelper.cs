using UnityEngine;
using UnityEngine.UI;

namespace Gem
{
	public static class UnityHelper
	{
		public static Vector2 ScaleInverse(this Vector2 thiz, Vector2 val)
		{
			return new Vector2(thiz.x / val.x, thiz.y / val.y);
		}

		public static Vector2 GetScreenSize()
		{
			return new Vector2(Screen.width, Screen.height);
		}

		public static Vector2 MouseProportionalPosition()
		{
			return ((Vector2) Input.mousePosition).ScaleInverse(GetScreenSize());
		}

		public static void SetSpriteAsNativeAndAssignPivot(this Image thiz, Sprite sprite)
		{
			thiz.sprite = sprite;
			thiz.SetNativeSize();
			thiz.rectTransform.pivot = sprite.pivot.ScaleInverse(sprite.rect.size);
		}

		public static void SetAlpha(this Graphic thiz, float val)
		{
			var newColor = thiz.color;
			newColor.a = val;
			thiz.color = newColor;
		}
	}
}

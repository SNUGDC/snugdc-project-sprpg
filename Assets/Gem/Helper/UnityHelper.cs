using UnityEngine;

namespace Gem
{
	public static class UnityHelper
	{
		public static Vector2 ScaleInverse(this Vector2 thiz, Vector2 val)
		{
			return new Vector2(thiz.x / val.x, thiz.y / val.y);
		}
	}
}

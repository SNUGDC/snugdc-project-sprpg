using UnityEngine;

namespace Gem
{
	public static class TransformHelper
	{
		public static void SetPos(this Transform thiz, Vector2 val)
		{
			var pos = thiz.position;
			pos.x = val.x;
			pos.y = val.y;
			thiz.position = pos;
		}

		public static void SetLPos(this Transform thiz, Vector2 val)
		{
			var pos = thiz.localPosition;
			pos.x = val.x;
			pos.y = val.y;
			thiz.localPosition = pos;
		}

		public static void SetPosX(this Transform thiz, float val)
		{
			var pos = thiz.position;
			pos.x = val;
			thiz.position = pos;
		}

		public static void SetPosY(this Transform thiz, float val)
		{
			var pos = thiz.position;
			pos.y = val;
			thiz.position = pos;
		}

		public static void SetPosZ(this Transform thiz, float val)
		{
			var pos = thiz.position;
			pos.z = val;
			thiz.position = pos;
		}

		public static void SetLPosX(this Transform thiz, float val)
		{
			var pos = thiz.localPosition;
			pos.x = val;
			thiz.localPosition = pos;
		}

		public static void SetLPosY(this Transform thiz, float val)
		{
			var pos = thiz.localPosition;
			pos.y = val;
			thiz.localPosition = pos;
		}

		public static void SetLPosZ(this Transform thiz, float val)
		{
			var pos = thiz.localPosition;
			pos.z = val;
			thiz.localPosition = pos;
		}

		public static void AddPos(this Transform thiz, Vector2 val)
		{
			var pos = thiz.position;
			pos += (Vector3)val;
			thiz.position = pos;
		}

		public static void AddLPos(this Transform thiz, Vector2 val)
		{
			var pos = thiz.localPosition;
			pos += (Vector3)val;
			thiz.localPosition = pos;
		}

		public static void LerpLPosX(this Transform thiz, float val, float lerp)
		{
			thiz.SetLPosX(Mathf.Lerp(thiz.localPosition.x, val, lerp));
		}

		public static void LerpLPosY(this Transform thiz, float val, float lerp)
		{
			thiz.SetLPosY(Mathf.Lerp(thiz.localPosition.y, val, lerp));
		}

		public static void LerpLPosZ(this Transform thiz, float val, float lerp)
		{
			thiz.SetLPosZ(Mathf.Lerp(thiz.localPosition.z, val, lerp));
		}

		public static float GetLScaleX(this Transform thiz)
		{
			return thiz.localScale.x;
		}

		public static void SetLScaleX(this Transform thiz, float val)
		{
			var scale = thiz.localScale;
			scale.x = val;
			thiz.localScale = scale;
		}

		public static float GetLScaleY(this Transform thiz)
		{
			return thiz.localScale.y;
		}

		public static void SetLScaleY(this Transform thiz, float val)
		{
			var scale = thiz.localScale;
			scale.y = val;
			thiz.localScale = scale;
		}

		public static float GetLScaleZ(this Transform thiz)
		{
			return thiz.localScale.z;
		}

		public static void SetLScaleZ(this Transform thiz, float val)
		{
			var scale = thiz.localScale;
			scale.z = val;
			thiz.localScale = scale;
		}

		public static float GetEulerX(this Transform thiz, float val)
		{
			return thiz.eulerAngles.x;
		}

		public static void SetEulerX(this Transform thiz, float val)
		{
			var euler = thiz.eulerAngles;
			euler.x = val;
			thiz.eulerAngles = euler;
		}

		public static float GetEulerY(this Transform thiz, float val)
		{
			return thiz.eulerAngles.y;
		}

		public static void SetEulerY(this Transform thiz, float val)
		{
			var euler = thiz.eulerAngles;
			euler.y = val;
			thiz.eulerAngles = euler;
		}

		public static float GetEulerZ(this Transform thiz, float val)
		{
			return thiz.eulerAngles.z;
		}

		public static void SetEulerZ(this Transform thiz, float val)
		{
			var euler = thiz.eulerAngles;
			euler.z = val;
			thiz.eulerAngles = euler;
		}

		public static float GetLEulerX(this Transform thiz)
		{
			return thiz.localEulerAngles.x;
		}

		public static void SetLEulerX(this Transform thiz, float val)
		{
			var euler = thiz.localEulerAngles;
			euler.x = val;
			thiz.localEulerAngles = euler;
		}

		public static float GetLEulerY(this Transform thiz)
		{
			return thiz.localEulerAngles.y;
		}

		public static void SetLEulerY(this Transform thiz, float val)
		{
			var euler = thiz.localEulerAngles;
			euler.y = val;
			thiz.localEulerAngles = euler;
		}

		public static float GetLEulerZ(this Transform thiz)
		{
			return thiz.localEulerAngles.z;
		}

		public static void SetLEulerZ(this Transform thiz, float val)
		{
			var euler = thiz.localEulerAngles;
			euler.z = val;
			thiz.localEulerAngles = euler;
		}

		public static void AddLEulerX(this Transform thiz, float val)
		{
			thiz.SetLEulerX(thiz.GetLEulerX() + val);
		}

		public static void AddLEulerY(this Transform thiz, float val)
		{
			thiz.SetLEulerY(thiz.GetLEulerY() + val);
		}

		public static void AddLEulerZ(this Transform thiz, float val)
		{
			thiz.SetLEulerZ(thiz.GetLEulerZ() + val);
		}

		public static float GetWidth(this RectTransform thiz)
		{
			return thiz.offsetMax.x - thiz.offsetMin.x;
		}
	}
}
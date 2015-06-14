using UnityEngine;

namespace Gem
{
	public static class TransformHelper
	{
		public static void SetPos(this Transform _this, Vector2 val)
		{
			var pos = _this.position;
			pos.x = val.x;
			pos.y = val.y;
			_this.position = pos;
		}

		public static void SetLPos(this Transform _this, Vector2 val)
		{
			var pos = _this.localPosition;
			pos.x = val.x;
			pos.y = val.y;
			_this.localPosition = pos;
		}

		public static void SetPosX(this Transform _this, float val)
		{
			var pos = _this.position;
			pos.x = val;
			_this.position = pos;
		}

		public static void SetPosY(this Transform _this, float val)
		{
			var pos = _this.position;
			pos.y = val;
			_this.position = pos;
		}

		public static void SetPosZ(this Transform _this, float val)
		{
			var pos = _this.position;
			pos.z = val;
			_this.position = pos;
		}

		public static void SetLPosX(this Transform _this, float val)
		{
			var pos = _this.localPosition;
			pos.x = val;
			_this.localPosition = pos;
		}

		public static void SetLPosY(this Transform _this, float val)
		{
			var pos = _this.localPosition;
			pos.y = val;
			_this.localPosition = pos;
		}

		public static void SetLPosZ(this Transform _this, float val)
		{
			var pos = _this.localPosition;
			pos.z = val;
			_this.localPosition = pos;
		}

		public static void AddPos(this Transform _this, Vector2 val)
		{
			var pos = _this.position;
			pos += (Vector3)val;
			_this.position = pos;
		}

		public static void AddLPos(this Transform _this, Vector2 val)
		{
			var pos = _this.localPosition;
			pos += (Vector3)val;
			_this.localPosition = pos;
		}

		public static void LerpLPosX(this Transform _this, float val, float lerp)
		{
			_this.SetLPosX(Mathf.Lerp(_this.localPosition.x, val, lerp));
		}

		public static void LerpLPosY(this Transform _this, float val, float lerp)
		{
			_this.SetLPosY(Mathf.Lerp(_this.localPosition.y, val, lerp));
		}

		public static void LerpLPosZ(this Transform _this, float val, float lerp)
		{
			_this.SetLPosZ(Mathf.Lerp(_this.localPosition.z, val, lerp));
		}

		public static float GetEulerX(this Transform _this, float val)
		{
			return _this.eulerAngles.x;
		}

		public static void SetEulerX(this Transform _this, float val)
		{
			var euler = _this.eulerAngles;
			euler.x = val;
			_this.eulerAngles = euler;
		}

		public static float GetEulerY(this Transform _this, float val)
		{
			return _this.eulerAngles.y;
		}

		public static void SetEulerY(this Transform _this, float val)
		{
			var euler = _this.eulerAngles;
			euler.y = val;
			_this.eulerAngles = euler;
		}

		public static float GetEulerZ(this Transform _this, float val)
		{
			return _this.eulerAngles.z;
		}

		public static void SetEulerZ(this Transform _this, float val)
		{
			var euler = _this.eulerAngles;
			euler.z = val;
			_this.eulerAngles = euler;
		}

		public static float GetLEulerX(this Transform _this)
		{
			return _this.localEulerAngles.x;
		}

		public static void SetLEulerX(this Transform _this, float val)
		{
			var euler = _this.localEulerAngles;
			euler.x = val;
			_this.localEulerAngles = euler;
		}

		public static float GetLEulerY(this Transform _this)
		{
			return _this.localEulerAngles.y;
		}

		public static void SetLEulerY(this Transform _this, float val)
		{
			var euler = _this.localEulerAngles;
			euler.y = val;
			_this.localEulerAngles = euler;
		}

		public static float GetLEulerZ(this Transform _this)
		{
			return _this.localEulerAngles.z;
		}

		public static void SetLEulerZ(this Transform _this, float val)
		{
			var euler = _this.localEulerAngles;
			euler.z = val;
			_this.localEulerAngles = euler;
		}

		public static void AddLEulerX(this Transform _this, float val)
		{
			_this.SetLEulerX(_this.GetLEulerX() + val);
		}

		public static void AddLEulerY(this Transform _this, float val)
		{
			_this.SetLEulerY(_this.GetLEulerY() + val);
		}

		public static void AddLEulerZ(this Transform _this, float val)
		{
			_this.SetLEulerZ(_this.GetLEulerZ() + val);
		}
	}
}
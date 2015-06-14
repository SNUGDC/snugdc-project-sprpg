using System.Diagnostics;
using UnityEngine;

namespace LitJson
{
	[DebuggerDisplay("({X}, {Y})")]
	public struct Point2
	{
		public int X;
		public int Y;

		public static implicit operator UnityEngine.Vector2(Point2 _this)
		{
			return new UnityEngine.Vector2(_this.X, _this.Y);
		}

		public static implicit operator UnityEngine.Vector3(Point2 _this)
		{
			return (UnityEngine.Vector2) _this;
		}
	}

	[DebuggerDisplay("({X}, {Y})")]
	public struct Vector2
	{
		public double X;
		public double Y;

		public static implicit operator UnityEngine.Vector2(Vector2 _this)
		{
			return new UnityEngine.Vector2((float)_this.X, (float)_this.Y);
		}

		public static implicit operator UnityEngine.Vector3(Vector2 _this)
		{
			return (UnityEngine.Vector2)_this;
		}
	}
}
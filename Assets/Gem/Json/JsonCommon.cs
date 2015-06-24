﻿using System.Diagnostics;
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

	[DebuggerDisplay("({MinX}, {MaxX}, {MinY}, {MaxY})")]
	public struct IntRect
	{
		public int MinX;
		public int MinY;
		public int MaxX;
		public int MaxY;

		public UnityEngine.Vector2 Min { get { return new UnityEngine.Vector2(MinX, MinY); } }
		public UnityEngine.Vector2 Max { get { return new UnityEngine.Vector2(MaxX, MaxY); } }

		public int Width { get { return MaxX - MinX; } }
		public int Height { get { return MaxY - MinY; } }

		public IntRect(int minX, int minY, int maxX, int maxY)
		{
			MinX = minX;
			MinY = minY;
			MaxX = maxX;
			MaxY = maxY;
		}

		public bool IsZero()
		{
			return MinX == 0 && MinY == 0
				&& MaxX == 0 && MaxY == 0;
		}

		public static implicit operator Rect(IntRect _this)
		{
			return new Rect(_this.MinX, _this.MinY, _this.Width, _this.Height);
		}
	}

	[DebuggerDisplay("({MinX}, {MaxX}, {MinY}, {MaxY})")]
	public struct FloatRect
	{
		public double MinX;
		public double MaxX;
		public double MinY;
		public double MaxY;

		public UnityEngine.Vector2 Min { get { return new UnityEngine.Vector2((float)MinX, (float)MinY); } }
		public UnityEngine.Vector2 Max { get { return new UnityEngine.Vector2((float)MaxX, (float)MaxY); } }

		public float Width { get { return (float)MaxX - (float)MinX; } }
		public float Height { get { return (float)MaxY - (float)MinY; } }

	}
}
using System.Diagnostics;

namespace LitJson
{
	[DebuggerDisplay("({X}, {Y})")]
	public struct Point2
	{
		public int X;
		public int Y;
	}

	[DebuggerDisplay("({X}, {Y})")]
	public struct Vector2
	{
		public double X;
		public double Y;
	}
}
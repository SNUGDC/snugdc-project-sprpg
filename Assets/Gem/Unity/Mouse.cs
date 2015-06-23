using UnityEngine;

namespace Gem
{
	public static class Mouse
	{
		private static Vector2 _currentPosition;
		private static Vector2 _previousPosition;

		public static Vector2 DeltaPosition { get { return _currentPosition - _previousPosition; } }

		public static void Update()
		{
			_previousPosition = _currentPosition;
			_currentPosition = Input.mousePosition;
		}
	}
}

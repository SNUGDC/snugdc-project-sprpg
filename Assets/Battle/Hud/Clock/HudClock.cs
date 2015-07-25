using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Battle
{
	public class HudClock : MonoBehaviour
	{
		private const int DegreePerTick = 360 / (int)Const.Period;

		[SerializeField]
		private Image _arrow;

		public void RefreshTime(Tick tick)
		{
			var rotation = _arrow.transform.eulerAngles;
			rotation.z = -(int)tick * DegreePerTick;
			_arrow.transform.eulerAngles = rotation;
		}
	}
}
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Battle
{
	public class HudClock : MonoBehaviour
	{
		[SerializeField]
		private Text _text;

		void Update()
		{
			var scheduler = HudController.Context.Scheduler;
			_text.text = scheduler.Relative.ToString();
			_text.color = scheduler.IsPerfectTerm ? Color.red : Color.black;
		}
	}
}
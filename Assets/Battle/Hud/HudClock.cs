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
			var clock = HudController.Context.PlayerClock;
			_text.text = clock.Relative.ToString();
			_text.color = clock.IsPerfectTerm ? Color.red : Color.black;
		}
	}
}
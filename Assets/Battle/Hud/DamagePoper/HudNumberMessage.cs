using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Battle.View
{
	public class HudNumberMessage : MonoBehaviour
	{
		private const float TimeToDestroy = 0.5f;

		[SerializeField]
		private Text _text;

		void Start()
		{
			Destroy(gameObject, TimeToDestroy);
		}

		public void Show(int num)
		{
			var message = num.ToString();
			if (num > 0) message = '+' + message;
			_text.text = message;
		}
	}
}
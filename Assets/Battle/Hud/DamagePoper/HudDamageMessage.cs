using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Battle.View
{
	public class HudDamageMessage : MonoBehaviour
	{
		private const float TimeToDestroy = 0.5f;

		[SerializeField]
		private Text _text;

		void Start()
		{
			Destroy(gameObject, TimeToDestroy);
		}

		public void Show(Damage damage)
		{
			_text.text = (-(int)damage.Value).ToString();
			_text.color = damage.Element.ToColor();
		}
	}
}
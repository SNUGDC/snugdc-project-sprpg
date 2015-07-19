using UnityEngine;
using UnityEngine.UI;

namespace SPRPG
{
	public class CharacterDescription : MonoBehaviour
	{
		[SerializeField]
		private Text _text;

		public void SetDescription(string value)
		{
			_text.text = value;
		}
	}
}

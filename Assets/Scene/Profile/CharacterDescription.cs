using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Profile
{
	public class CharacterDescription : MonoBehaviour
	{
		public Text Text;

		public void SetDescription(string value)
		{
			Text.text = value;
		}
	}
}

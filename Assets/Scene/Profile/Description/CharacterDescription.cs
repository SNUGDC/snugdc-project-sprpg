using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Profile
{
	public class CharacterDescription : MonoBehaviour
	{
		[SerializeField]
		private Text _name;
		[SerializeField]
		private Text _description;

		public void Refresh(CharacterBalanceData data)
		{
			SetName(data.Name);
			SetDescription(data.FlavorText);
		}

		private void SetName(string value)
		{
			_name.text = value;
		}

		private void SetDescription(string value)
		{
			_description.text = value;
		}
	}
}

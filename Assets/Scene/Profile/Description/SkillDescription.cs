using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Profile
{
	public class SkillDescription : MonoBehaviour
	{
		[SerializeField]
		private Text _name;
		[SerializeField]
		private Text _description;

		public void Refresh(SkillBalanceData data)
		{
			SetName(data.Name);
			SetDescription(data.Describe());
		}

		public void SetName(string value)
		{
			_name.text = value;
		}

		public void SetDescription(string value)
		{
			_description.text = value;
		}
	}
}

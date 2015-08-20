using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Profile
{
	public class SkillDescription : MonoBehaviour
	{
		[SerializeField]
		private Text _text;

		public void SetDescription(SkillBalanceData data)
		{
			_text.text = data.Describe();
		}
	}
}

using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Profile
{
	public class SkillDescription : MonoBehaviour
	{
		[SerializeField]
		private Text _text;

		public void SetDescription(SkillKey key)
		{
			var detail = SkillBalance._.Find(key);
			_text.text = detail.Describe();
		}
	}
}

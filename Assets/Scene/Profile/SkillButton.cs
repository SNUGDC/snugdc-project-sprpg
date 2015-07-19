using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Profile
{
	public class SkillButton : MonoBehaviour
	{
		[SerializeField]
		private Image _icon;

		public void SetIcon(SkillKey key)
		{
			var data = SkillBalance._.Find(key);
			var icon = Resources.Load<Sprite>(data.Icon);
			_icon.sprite = icon;
		}
	}
}

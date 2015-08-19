using System;
using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Profile
{
	public class SkillSlot : MonoBehaviour
	{
		private SkillKey _key;

		[SerializeField]
		private Image _icon;

		public Action<SkillKey> OnClickCallback;
		
		public void SetIcon(SkillKey key)
		{
			_key = key;
			var data = SkillBalance._.Find(key);
			var icon = Resources.Load<Sprite>(data.Icon);
			_icon.sprite = icon;
		}

		public void OnClick()
		{
			OnClickCallback.CheckAndCall(_key);
		}
	}
}

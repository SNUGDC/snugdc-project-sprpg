using System;
using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Profile
{
	public class SkillSlot : MonoBehaviour
	{
		private SkillKey? _key;

		[SerializeField]
		private Image _icon;

		public Action<SkillKey> OnClickCallback;
		
		public void Set(SkillKey key)
		{
			_key = key;
			_icon.enabled = true;
			_icon.sprite = R.Skill.GetIcon(key);
		}

		public void Clear()
		{
			_key = null;
			_icon.enabled = false;
			Deselect();
		}

		public void Select()
		{
			_icon.color = Color.white;
		}

		public void Deselect()
		{
			_icon.color = Color.grey;
		}

		public void OnClick()
		{
			if (_key == null) return;
			OnClickCallback.CheckAndCall(_key.Value);
		}
	}
}

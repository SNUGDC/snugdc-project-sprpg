using System;
using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Profile
{
	public class SkillSlot : MonoBehaviour
	{
		public bool IsSelected { get; private set; }

		private SkillKey? _key;

		[SerializeField]
		private Image _icon;

		public Action<SkillKey, bool> OnClickCallback;

		void Start()
		{
			// deselect again.
			if (!IsSelected) ForceDeselect();
		}

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

		public void Select(bool isSelected)
		{
			if (isSelected)
				Select();
			else 
				Deselect();
		}

		public void Select()
		{
			if (IsSelected) return;
			IsSelected = true;
			_icon.color = Color.white;
		}

		public void Deselect()
		{
			if (!IsSelected) return;
			ForceDeselect();
		}

		private void ForceDeselect()
		{
			IsSelected = false;
			_icon.color = Color.grey;
		}
			
		public void OnClick()
		{
			if (_key == null) return;
			OnClickCallback.CheckAndCall(_key.Value, IsSelected);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Gem;
using UnityEngine;

namespace SPRPG.Profile
{
	public class SkillSlots : MonoBehaviour
	{
		private CharacterId? _character;

		[SerializeField] 
		private List<SkillSlot> _slots;
		
		public Action<SkillKey, bool> OnClickCallback;

		void Start()
		{
			foreach (var slot in _slots)
				slot.OnClickCallback += OnClick;
		}

		public void Show(CharacterId character)
		{
			_character = character;

			var skills = SkillBalance._.SelectCharacterSpecifics(character);

			var userSkillSet = new List<SkillKey>();
			var userCharacter = UserCharacters.Find(character);
			if (userCharacter != null) userSkillSet = userCharacter.SkillSet.ToList();

			var i = 0;
			for (; i < _slots.Count && i < skills.Count; ++i)
			{
				var skillKey = skills[i].Key;
				var slot = _slots[i];
				slot.Set(skillKey);
				if (userSkillSet.Contains(skillKey))
					slot.Select();
				else 
					slot.Deselect();
			}

			for (; i < _slots.Count; ++i)
				_slots[i].Clear();
		}

		public void Refresh()
		{
			if (_character == null)
			{
				Debug.LogError("refresh before show.");
				return;
			}
			Show(_character.Value);
		}

		private void OnClick(SkillKey skill, bool isSelected)
		{
			OnClickCallback.CheckAndCall(skill, isSelected);
		}
	}
}

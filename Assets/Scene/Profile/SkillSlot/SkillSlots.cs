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
		private List<SkillSlotRow> _slots;

		public Action<SkillKey, bool> OnClickCallback;

		void Start()
		{
			foreach (var slotRow in _slots)
				OnClickCallback += slotRow.OnClickCallback;
		}

		public void Show(CharacterId character)
		{
			Clear();

			_character = character;
			var skills = SkillBalance._.SelectCharacterSpecifics(character);
			var userSkillSet = new List<SkillKey>();
			var userCharacter = UserCharacters.Find(character);
			if (userCharacter != null) userSkillSet = userCharacter.SkillSet.ToList();

			for (var i = 0; i < skills.Count; ++i)
			{
				var skillKey = skills[i].Key;
				var skillData = SkillBalance._.Find(skillKey);
				var slotRow = _slots[skillData.Tier.ToIndex()];
				var slot = slotRow.Add(skillKey);
				slot.Select(userSkillSet.Contains(skillKey));
			}

			foreach (var slotRow in _slots)
				slotRow.Align();
		}

		public void Clear()
		{
			foreach (var slotRow in _slots)
				slotRow.Clear();
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

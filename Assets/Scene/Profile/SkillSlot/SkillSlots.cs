using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SPRPG.Profile
{
	public class SkillSlots : MonoBehaviour
	{
		[SerializeField] 
		private List<SkillSlot> _slots;
		
		public Action<SkillKey> OnClick;

		void Start()
		{
			foreach (var slot in _slots)
			{
				slot.OnClickCallback += OnClick;
			}
		}

		public void Show(CharacterId character)
		{
			var skills = SkillBalance._.SelectCharacterSpecifics(character);

			var userSkillSet = new List<SkillKey>();
			var userCharacter = UserCharacters.Find(character);
			if (userCharacter != null) userSkillSet = userCharacter.SkillSet.ToList();

			foreach (var slot in _slots)
				slot.Clear();

			for (var i = 0; i < _slots.Count && i < skills.Count; ++i)
			{
				var skillKey = skills[i].Key;
				var slot = _slots[i];
				slot.Set(skillKey);
				if (userSkillSet.Contains(skillKey))
					slot.Select();
			}
		}
	}
}

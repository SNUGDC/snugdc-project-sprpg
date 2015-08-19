using System;
using System.Collections.Generic;
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

			var i = 0;
			for (; i < _slots.Count && i < skills.Count; ++i)
				_slots[i].Set(skills[i].Key);

			for (; i < _slots.Count; ++i)
				_slots[i].Clear();
		}
	}
}

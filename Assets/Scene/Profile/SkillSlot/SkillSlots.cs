using System;
using System.Collections.Generic;
using Gem;
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

		public void SetSlot(SPRPG.SkillSlot num, SkillKey key)
		{
			_slots[num.ToIndex()].SetIcon(key);  
		}
	}
}

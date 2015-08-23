using System;
using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG.Profile
{
	public class SkillSlotRow : MonoBehaviour
	{
		[SerializeField]
		private SkillSlot _slotPrefab;
		private readonly List<SkillSlot> _slots = new List<SkillSlot>();

		public Action<SkillKey, bool> OnClickCallback;

		public SkillSlot Add(SkillKey skill)
		{
			var slot = _slotPrefab.Instantiate();
			slot.transform.SetParent(transform, false);
			slot.Set(skill);
			slot.OnClickCallback += OnClickCallback;
			_slots.Add(slot);
			return slot;
		}

		public void Align()
		{
			var i = 0;
			var count = _slots.Count;
			foreach (var slot in _slots)
			{
				var align = i++ - count/2f;
				slot.transform.SetLPosX(align * 100);
			}
		}

		public void Clear()
		{
			foreach (var slot in _slots)
				Destroy(slot.gameObject);
			_slots.Clear();
		}
	}
}

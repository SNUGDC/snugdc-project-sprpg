﻿using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Battle
{
	public class HudClock : MonoBehaviour
	{
		[SerializeField]
		private Image[] _slots;
		[SerializeField]
		private RawImage _glow;
		[SerializeField]
		private Image _skillIcon;

		private static float CalculateTiming(float progress)
		{
			var x = 2*progress - 1;
			return 1 - x*x;
		}

		public void Refresh(SkillKey skill, SkillSlot slot, float progress)
		{
			progress = Mathf.Max(0, progress);
			var timing = CalculateTiming(progress);
			RefreshSlots(slot);
			RefreshCurrentSlot(slot, timing);
			RefreshGlow(timing);
			RefreshSkill(skill, timing);
		}

		private void RefreshSlots(SkillSlot slot)
		{
			var slotIdx = slot.ToIndex();
			for (int i = 0; i != SPRPG.Const.SkillSlotSize; ++i)
				_slots[i].gameObject.SetActive(i == slotIdx);
		}

		private void RefreshCurrentSlot(SkillSlot slot, float timing)
		{
			var slotImage = _slots[slot.ToIndex()];
			slotImage.SetAlpha(timing);
		}

		private void RefreshGlow(float timing)
		{
			_glow.SetAlpha(timing);
		}

		private void RefreshSkill(SkillKey skill, float timing)
		{
			_skillIcon.transform.localScale = Vector3.one*timing;
			_skillIcon.sprite = R.Skill.GetIcon(skill);
		}
	}
}
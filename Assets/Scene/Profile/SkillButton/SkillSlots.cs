using System.Collections.Generic;
using UnityEngine;

namespace SPRPG.Profile
{
	public class SkillSlots : MonoBehaviour
	{
		[SerializeField] 
		private List<SkillButton> _slots;

		public void SetSlot(SkillSlot num, SkillKey key)
		{
			_slots[num.ToIndex()].SetIcon(key);  
		}
	}
}

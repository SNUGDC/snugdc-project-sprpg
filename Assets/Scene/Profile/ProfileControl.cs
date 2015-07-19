using UnityEngine;

namespace SPRPG.Profile
{
	public class ProfileControl : MonoBehaviour
	{
		[SerializeField]
		private SkillSlots _slots;

		void Start()
		{
			_slots.SetSlot(SkillSlot._1, SkillKey.WarriorAttack);
			_slots.SetSlot(SkillSlot._2, SkillKey.WarriorEvasion);
			_slots.SetSlot(SkillSlot._3, SkillKey.WarriorHeal);
			_slots.SetSlot(SkillSlot._4, SkillKey.WarriorStrongAttack);
		}

		public void Back()
		{
			Transition.TransferToCamp();
		}

		public void TransferToSetting()
		{
			Transition.TransferToSetting();
		}		
	}
}

using UnityEngine;

namespace SPRPG.Profile
{
	public class ProfileControl : MonoBehaviour
	{
		[SerializeField]
		private SkillSlots _slots;

		[SerializeField]
		private SkillDescription _skillDescription;

		void Start()
		{
			_slots.OnClick += key => _skillDescription.SetDescription(key);
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

using UnityEngine;

namespace SPRPG.Profile
{
	public class ProfileController : MonoBehaviour
	{
		public static CharacterId? CharacterToShow;

		public CharacterId? Character { get; private set; }

		[SerializeField]
		private CharacterDescription _characterDescription;
		[SerializeField]
		private CharacterShower _characterShower;

		[SerializeField]
		private SkillSlots _skillSlots;
		[SerializeField]
		private SkillDescription _skillDescription;

		void Start()
		{
			if (CharacterToShow != null) Show(CharacterToShow.Value);
			_skillSlots.OnClickCallback += OnSkillSlotClicked;
		}

		public void Show(CharacterId character)
		{
			Character = character;
			var data = CharacterDb._.Find(character);
			_characterDescription.Refresh(data.Balance);
			_characterShower.Show(data);
			_skillSlots.Show(character);
		}

		public void Back()
		{
			Transition.TransferToCamp();
		}

		public void TransferToSetting()
		{
			Transition.TransferToSetting();
		}

		private void OnSkillSlotClicked(SkillKey skill, bool isSelected)
		{
			if (Character == null) return;
			var data = SkillBalance._.Find(skill);
			_skillDescription.Refresh(data);
			
			var userCharacter = UserCharacters.Find(Character.Value);
			if (userCharacter != null)
			{
				userCharacter.SkillSet[data.Tier.ToSlot()] = skill;
				_skillSlots.Refresh();
			}
		}
	}
}

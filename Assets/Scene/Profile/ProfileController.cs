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
			_skillSlots.OnClick += key => _skillDescription.SetDescription(key);
		}

		public void Show(CharacterId character)
		{
			Character = character;
			var data = CharacterDb._.Find(character);
			_characterDescription.SetDescription(data.Detail);
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
	}
}

using Gem;
using UnityEngine;

namespace SPRPG.Profile
{
	public class CharacterShower : MonoBehaviour
	{
		private CharacterView _characterView;

		public void Show(CharacterData data)
		{
			if (_characterView != null) Destroy(_characterView.gameObject);
			_characterView = data.CharacterView.Instantiate();
			_characterView.transform.SetParent(transform, false);
		}

		public void PlaySkillStart(SkillBalanceData data)
		{
			_characterView.PlaySkillStart(data, null);
		}
	}
}

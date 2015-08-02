using Gem;
using UnityEngine;

namespace SPRPG.Camp
{
	public class CargoEntry : MonoBehaviour
	{
		private CharacterView CharacterView;

		public void SetCharacter(CharacterId id)
		{
			var characterData = CharacterDb._.Find(id);
			if (characterData == null) return;
			if (CharacterView != null) Destroy(CharacterView.gameObject);
			CharacterView = characterData.CharacterView.Instantiate();
			CharacterView.transform.SetParent(transform, false);
			CharacterView.transform.localPosition = Vector3.zero;
		}

		public void RemoveCharacter()
		{
			if (CharacterView == null)
			{
				Debug.LogError("view not exist.");
				return;
			}

			Destroy(CharacterView.gameObject);
			CharacterView = null;
		}
	}
}
using Gem;
using UnityEngine;

namespace SPRPG.Camp
{
	public class CargoEntry : MonoBehaviour
	{
		private CharacterSkeleton Skeleton;

		void Start()
		{
			Skeleton = Assets._.CharacterSkeleton.Instantiate();
			Skeleton.transform.SetParent(transform, false);
			Skeleton.transform.localPosition = Vector3.zero;
			Skeleton.gameObject.SetActive(false);
		}

		public void SetCharacter(CharacterID id)
		{
			var characterData = CharacterDB._.Find(id);
			if (characterData == null) return;
			Skeleton.gameObject.SetActive(true);
			Skeleton.SetSkin(characterData.Skin);
		}

		public void RemoveCharacter()
		{
			Skeleton.gameObject.SetActive(false);
		}
	}
}
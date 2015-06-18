using UnityEngine;

namespace SPRPG
{
	public class CharacterData : ScriptableObject
	{
		[SerializeField]
		private int _id;
		public CharacterID ID
		{
			get { return (CharacterID) _id; }
			set { _id = (int) value; }
		}

		public CharacterSkinData Skin;

		public Sprite CampEntryIcon;
		public AnimatorOverrideController CampAnimatorController;
	}
}
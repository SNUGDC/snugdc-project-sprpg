using UnityEngine;

namespace SPRPG
{
	public class CharacterData : ScriptableObject
	{
		[HideInInspector] public CharacterID ID;
		public CharacterSkinData Skin;
	}
}
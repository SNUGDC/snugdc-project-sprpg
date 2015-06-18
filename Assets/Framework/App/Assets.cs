using UnityEngine;

namespace SPRPG
{
	public class Assets : MonoBehaviour
	{
		public static Assets _ { get; private set; }

		public CharacterSkeleton CharacterSkeleton;

		public Sprite CampEntryEmptySprite;
		public CampCharacter CampCharacter;
		public CampCharacterTool CampCharacterTool;

		public static void Init(Assets assets)
		{
			_ = assets;
		}
	}
}

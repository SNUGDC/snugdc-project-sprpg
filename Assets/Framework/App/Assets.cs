using UnityEngine;

namespace SPRPG
{
	public class Assets : MonoBehaviour
	{
		public static Assets _ { get; private set; }

	    public TextAsset WorldDb;

		public CharacterSkeleton CharacterSkeleton;

		public Sprite CampEntryEmptySprite;
		public Camp.CampCharacter CampCharacter;
		public Camp.CharacterTool CampCharacterTool;

		public Battle.View.CharacterView CharacterView;

		public static void Init(Assets assets)
		{
			_ = assets;
		}
	}
}

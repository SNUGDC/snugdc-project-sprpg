using UnityEngine;

namespace SPRPG
{
	public class Assets : MonoBehaviour
	{
		public static Assets _ { get; private set; }

		public CharacterSkeleton CharacterSkeleton;

		public CampCharacter CampCharacter;

		public static void Init(Assets assets)
		{
			_ = assets;
		}
	}
}

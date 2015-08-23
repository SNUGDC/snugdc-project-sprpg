using UnityEngine;

namespace SPRPG
{
	public class Assets : MonoBehaviour
	{
		public static Assets _ { get; private set; }

		public GameObject FxStatusConditionPoison;
		public GameObject FxCharacterGuardAura;

		public Sprite CampEntryEmptySprite;
		public Camp.CampCharacter CampCharacter;
		public Camp.CharacterTool CampCharacterTool;

		public Battle.View.ResultWinPopup ResultWinPopup;
		public Battle.View.ResultLosePopup ResultLosePopup;

		public static void Init(Assets assets)
		{
			_ = assets;
		}
	}
}

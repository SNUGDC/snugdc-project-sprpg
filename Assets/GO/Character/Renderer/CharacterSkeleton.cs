using UnityEngine;

namespace SPRPG
{
	public class CharacterSkeleton : MonoBehaviour
	{
		public SpriteRenderer Body;
		public SpriteRenderer ArmLU;
		public SpriteRenderer ArmLL;
		public SpriteRenderer ArmRU;
		public SpriteRenderer ArmRL;
		public SpriteRenderer LegLU;
		public SpriteRenderer LegLL;
		public SpriteRenderer LegRU;
		public SpriteRenderer LegRL;

		public Transform HandL;
		public Transform HandR;
		public Transform FeetL;
		public Transform FeetR;

		public void SetSkin(CharacterSkinData skin)
		{
			Body.sprite = skin.Body;
			ArmLU.sprite = skin.ArmLU;
			ArmLL.sprite = skin.ArmLL;
			ArmRU.sprite = skin.ArmRU;
			ArmRL.sprite = skin.ArmRL;
			LegLU.sprite = skin.LegLU;
			LegLL.sprite = skin.LegLL;
			LegRU.sprite = skin.LegRU;
			LegRL.sprite = skin.LegRL;
		}
	}
}
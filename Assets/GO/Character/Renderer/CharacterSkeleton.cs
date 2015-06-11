using UnityEngine;

namespace SPRPG
{
	public class CharacterSkeleton : MonoBehaviour
	{
		public SpriteRenderer Hip;
		public SpriteRenderer Chest;

		public SpriteRenderer Head;
		public SpriteRenderer EyeL;
		public SpriteRenderer EyeR;

		public SpriteRenderer ArmLU;
		public SpriteRenderer ArmLL;
		public SpriteRenderer HandL;

		public SpriteRenderer ArmRU;
		public SpriteRenderer ArmRL;
		public SpriteRenderer HandR;

		public SpriteRenderer LegLU;
		public SpriteRenderer LegLL;

		public SpriteRenderer LegRU;
		public SpriteRenderer LegRL;

		public void SetSkin(CharacterSkinData skin)
		{
			Hip.sprite = skin.Hip;
			Chest.sprite = skin.Chest;

			Head.sprite = skin.Head;
			EyeL.sprite = skin.EyeL;
			EyeR.sprite = skin.EyeR;

			ArmLU.sprite = skin.ArmLU;
			ArmLL.sprite = skin.ArmLL;
			HandL.sprite = skin.HandL;

			ArmRU.sprite = skin.ArmRU;
			ArmRL.sprite = skin.ArmRL;
			HandR.sprite = skin.HandR;

			LegLU.sprite = skin.LegLU;
			LegLL.sprite = skin.LegLL;

			LegRU.sprite = skin.LegRU;
			LegRL.sprite = skin.LegRL;
		}
	}
}
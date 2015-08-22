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

		public void SetColorRecursive(Color color)
		{
			foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
				renderer.color = color;
		}
	}
}
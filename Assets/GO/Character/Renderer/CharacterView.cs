using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterView : MonoBehaviour
	{
		[SerializeField]
		private CharacterSkeleton _skeleton;
		[SerializeField]
		protected Transform FxRoot;
		public Animator Animator;

		public void Attack()
		{
			Animator.SetTrigger("Attack");
		}

		public void Dead()
		{
			Animator.SetTrigger("Dead");
		}

		public virtual void PlaySkillStart(SkillBalanceData data) { }
				
		public void InstantiateFx(GameObject fxPrefab)
		{
			var fx = fxPrefab.Instantiate ();
			fx.transform.SetParent (FxRoot, false);
		}
	}
}
using UnityEngine;

namespace SPRPG
{
	public class CharacterView : MonoBehaviour
	{
		[SerializeField]
		private CharacterId _id;
		public CharacterId Id { get { return _id; } }
		[SerializeField]
		private CharacterSkeleton _skeleton;
		[SerializeField]
		protected CharacterViewPoints Points; 
		public Animator Animator;

		void Start()
		{
#if UNITY_EDITOR
			gameObject.AddComponent<CharacterViewDebugger>();
#endif
		}

		public void Attack() { Animator.SetTrigger("Attack"); }
		public void StrongAttack() { Animator.SetTrigger("StrongAttack"); }
		public void Dead() { Animator.SetTrigger("Dead"); }
		public void Buff() { Animator.SetTrigger("Buff"); }
		public void Jump() { Animator.SetTrigger("Jump"); }
		public void ShakeHorizontal() { Animator.SetTrigger("ShakeHorizontal"); }

		public virtual void PlaySkillStart(SkillBalanceData data) { }
	}
}
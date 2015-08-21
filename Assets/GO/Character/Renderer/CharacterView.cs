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
		public void Dead() { Animator.SetTrigger("Dead"); }

		public virtual void PlaySkillStart(SkillBalanceData data) { }
	}
}
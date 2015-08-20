using SPRPG.Battle;
using UnityEngine;

namespace SPRPG
{
	public class CharacterView : MonoBehaviour
	{
		[SerializeField]
		private CharacterSkeleton _skeleton;
		[SerializeField]
		private Animator _animator;

		public void Attack()
		{
			_animator.SetTrigger("Attack");
		}

		public void Dead()
		{
			_animator.SetTrigger("Dead");
		}

		public void PlaySkillStart(SkillBalanceData data)
		{
		}
	}
}
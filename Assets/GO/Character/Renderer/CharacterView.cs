﻿using UnityEngine;

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

		private GameObject _guardAura;

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

		public void PlayGuardAura()
		{
			if (_guardAura != null)
			{
				Debug.LogWarning("already has guard aura.");
				return;
			}

			_guardAura = Points.InstantiateOnCenter(Assets._.FxCharacterGuardAura);
		}

		public void StopGuardAura()
		{
			if (_guardAura == null)
			{
				Debug.LogError("no guard one.");
				return;
			}

			Destroy(_guardAura.gameObject);
		}

		public virtual void PlaySkillStart(SkillBalanceData data) { }
	}
}
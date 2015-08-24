using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterView : MonoBehaviour
	{
		public Battle.View.BattleController Context;

		[SerializeField]
		private CharacterId _id;
		public CharacterId Id { get { return _id; } }
		[SerializeField]
		private CharacterSkeleton _skeleton;
		public CharacterSkeleton Skeleton { get { return _skeleton; } }
		public CharacterViewPoints Points; 
		public Animator Animator;

		private GameObject _fxGuardAura;
		private GameObject _fxStatusConditionPoison;

		void Start()
		{
#if UNITY_EDITOR
			gameObject.AddComponent<CharacterViewDebugger>();
#endif
		}

		public void Attack() { Animator.SetTrigger("Attack"); }
		public void StrongAttack() { Animator.SetTrigger("StrongAttack"); }
		public void Hit() { Animator.SetTrigger("Hit"); }
		public void Dead() { Animator.SetTrigger("Dead"); }
		public void Buff() { Animator.SetTrigger("Buff"); }
		public void Jump() { Animator.SetTrigger("Jump"); }
		public void ShakeHorizontal() { Animator.SetTrigger("ShakeHorizontal"); }

		public void PlayGuardAura()
		{
			if (_fxGuardAura != null)
			{
				Debug.LogWarning("already has guard aura.");
				return;
			}

			_fxGuardAura = Points.InstantiateOnCenter(Assets._.FxCharacterGuardAura);
		}

		public void StopGuardAura()
		{
			if (_fxGuardAura == null)
			{
				Debug.LogError("no guard one.");
				return;
			}

			Destroy(_fxGuardAura.gameObject);
		}

		public void PlayStatusConditionPoison()
		{
			Animator.SetTrigger("StatusConditionPoisonStart");

			if (_fxStatusConditionPoison != null)
			{
				Debug.LogError("already has fx poison.");
				return;
			}

			_fxStatusConditionPoison = Points.InstantiateOnCenter(Assets._.FxStatusConditionPoison);
		}

		public void StopStatusConditionPoison()
		{
			Animator.SetTrigger("StatusConditionPoisonStop");

			if (_fxStatusConditionPoison == null)
			{
				Debug.LogError("no fx poison.");
				return;
			}

			Destroy(_fxStatusConditionPoison.gameObject);
		}

		public virtual void PlaySkillStart(SkillBalanceData data, object argument) { }
	}
}
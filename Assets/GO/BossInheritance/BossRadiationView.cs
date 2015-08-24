using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class BossRadiationView : BossView
	{
		public GameObject FxAttack;
		public GameObject FxRangeAttackLockOn;
		public GameObject FxRangeAttackShoot;
		public GameObject FxRangeAttackHit;
		public GameObject FxPoisonExplosion;

		private readonly List<ShiftedPartyIdx> _rangeAttackTargets = new List<ShiftedPartyIdx>(3);
		private readonly List<GameObject> _rangeAttackLockOns = new List<GameObject>(3);

		public override void PlaySkillStart(BossSkillBalanceData data, object arguments)
		{
			switch (data.Key)
			{
				case BossSkillLocalKey.Recovery:
					PlayRecovery();
					return;

				case BossSkillLocalKey.Attack1:
				case BossSkillLocalKey.Attack2:
				case BossSkillLocalKey.Attack3:
					PlayAttackStart();
					return;

				case BossSkillLocalKey.RangeAttack1:
				case BossSkillLocalKey.RangeAttack2:
				case BossSkillLocalKey.RangeAttack3:
					PlayRangeAttack((List<ShiftedPartyIdx>)arguments);
					return;

				case BossSkillLocalKey.PoisonExplosion1:
				case BossSkillLocalKey.PoisonExplosion2:
					PlayPoisonExplosion((List<Character>)arguments);
					return;

				default:
					Debug.LogWarning(LogMessages.EnumNotHandled(data.Key));
					return;
			}
		}

		public void PlayRecovery()
		{
			Animator.SetTrigger("Recovery");
		}

		public void PlayAttackStart()
		{
			Animator.SetTrigger("Attack");
		}

		public void PlayAttackEffect()
		{
			Points.InstantiateOnFrontGround(FxAttack);
		}

		public void PlayRangeAttack(List<ShiftedPartyIdx> targets)
		{
			Animator.SetTrigger("RangeAttack");

			if (targets == null)
			{
				Debug.LogError("no targets specified.");
				targets = new List<ShiftedPartyIdx>(3) { ShiftedPartyIdx._1, ShiftedPartyIdx._2, ShiftedPartyIdx._3 };
			}

			_rangeAttackTargets.Clear();
			_rangeAttackTargets.AddRange(targets);

			foreach (var target in targets)
			{
				var character = BattlePoints._.Characters[target.ToArrayIndex()];
				var lockOn = character.InstantiateOnCenter(FxRangeAttackLockOn);
				_rangeAttackLockOns.Add(lockOn);
			}
		}

		public void PlayRangeAttackFxShoot()
		{
			Points.InstantiateOnAboveHead(FxRangeAttackShoot);
		}

		public void PlayRangeAttackFxHit()
		{
			foreach (var target in _rangeAttackTargets)
			{
				var character = BattlePoints._.Characters[target.ToArrayIndex()];
				character.InstantiateOnBottom(FxRangeAttackHit);
			}
		}

		public void ClearRangeAttackLockOnAfterDelay()
		{
			Invoke("ClearRangeAttackLockOn", 0.2f);
		}

		private void ClearRangeAttackLockOn()
		{
			foreach (var lockOn in _rangeAttackLockOns)
				Destroy(lockOn);
			_rangeAttackLockOns.Clear();
		}

		private void PlayPoisonExplosion(List<Character> targets)
		{
			if (Context == null) return;
			if (targets == null) return;

			foreach (var target in targets)
			{
				var characterView = Context.FindCharacterView(target);
				characterView.Points.InstantiateOnCenter(FxPoisonExplosion);
			}
		}
	}
}
using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class BossRadiationView : BossView
	{
		public GameObject FxAttack;
		public GameObject FxRangeAttackShoot;

		public override void PlaySkillStart(BossSkillBalanceData data, object arguments)
		{
			switch (data.Key)
			{
				case BossSkillLocalKey.Attack1:
				case BossSkillLocalKey.Attack2:
				case BossSkillLocalKey.Attack3:
					PlayAttackStart();
					return;

				case BossSkillLocalKey.RangeAttack1:
				case BossSkillLocalKey.RangeAttack2:
				case BossSkillLocalKey.RangeAttack3:
					PlayRangeAttack();
					return;

				default:
					Debug.LogWarning(LogMessages.EnumNotHandled(data.Key));
					return;
			}
		}

		public void PlayAttackStart()
		{
			Animator.SetTrigger("Attack");
		}

		public void PlayAttackEffect()
		{
			Points.InstantiateOnFrontGround(FxAttack);
		}

		public void PlayRangeAttack()
		{
			Animator.SetTrigger("RangeAttack");
		}

		public void PlayRangeAttackFxShoot()
		{
			Points.InstantiateOnAboveHead(FxRangeAttackShoot);
		}
	}
}
using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class BossRadiationView : BossView
	{
		public override void PlaySkillStart(BossSkillBalanceData data, object arguments)
		{
			switch (data.Key)
			{
				case BossSkillLocalKey.Attack1:
				case BossSkillLocalKey.Attack2:
				case BossSkillLocalKey.Attack3:
					PlayAttack();
					return;

				default:
					Debug.LogWarning(LogMessages.EnumNotHandled(data.Key));
					return;
			}
		}

		public void PlayAttack()
		{
			Animator.SetTrigger("Attack");
		}
	}
}
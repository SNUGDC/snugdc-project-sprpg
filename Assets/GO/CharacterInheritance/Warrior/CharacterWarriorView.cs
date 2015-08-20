using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterWarriorView : CharacterView
	{
		public GameObject FxAttck;
		public GameObject FxHeal;
		public GameObject FxStrongAttack;

		public override void PlaySkillStart(SkillBalanceData data)
		{
			switch (data.Key) 
			{
				case SkillKey.WarriorAttack:
					PlayAttack();
					return;
				case SkillKey.WarriorHeal:
					PlayHeal();
					return;
				case SkillKey.WarriorStrongAttack:
					PlayStrongAttack();
					return;
				case SkillKey.WarriorEvasion:
					PlayEvasion();
					return;
				default:
					return;
			}
		}
	
		public void PlayAttack()
		{
			Animator.SetTrigger ("Attack");
			var fxAttack = InstantiateFx (FxAttck);
			fxAttack.transform.Translate (new Vector2 (1, 0.5f));
		}

		public void PlayHeal()
		{
			Animator.SetTrigger ("Buff");
			var fx = InstantiateFx (FxHeal);
			fx.transform.localPosition = new Vector2 (0, 1.5f);
		}

		public void PlayStrongAttack()
		{
			Animator.SetTrigger ("StrongAttack");
			Invoke ("PlayDelayedStrongAttack", 0.3f);
		}
		public void PlayDelayedStrongAttack()
		{
			var fx = InstantiateFx (FxStrongAttack);
			fx.transform.localPosition = new Vector2 (1.8f, 0);
		}
		public void PlayEvasion()
		{
			Animator.SetTrigger ("ShakeHorizontal");
		}
	}
}
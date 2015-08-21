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
				case SkillKey.WarriorAttack: PlayAttack(); return;
				case SkillKey.WarriorHeal: PlayHeal(); return;
				case SkillKey.WarriorStrongAttack: PlayStrongAttack(); return;
				case SkillKey.WarriorEvasion: PlayEvasion(); return;
				default: Debug.LogError(LogMessages.EnumNotHandled(data.Key)); return;
			}
		}
	
		public void PlayAttack()
		{
			Attack();
			Points.InstantiateOnAim(FxAttck);
		}

		public void PlayHeal()
		{
			Buff();
			Points.InstantiateOnBuffTop(FxHeal);
		}

		public void PlayStrongAttack()
		{
			StrongAttack();
			Invoke ("PlayDelayedStrongAttack", 0.3f);
		}

		public void PlayDelayedStrongAttack()
		{
			// todo: instantiate on world.
			var fx = Points.InstantiateOnAim(FxStrongAttack);
			fx.transform.localPosition = new Vector2 (1.8f, -0.7f);
		}

		public void PlayEvasion()
		{
			ShakeHorizontal();
		}
	}
}
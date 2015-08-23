using Gem;
using SPRPG.Battle.View;
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
			Jump();
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
			Jump();
			Invoke("PlayDelayedStrongAttack", 0.3f);
		}

		public void PlayDelayedStrongAttack()
		{
			BattlePoints._.Boss.InstantiateOnBottom(FxStrongAttack);
		}

		public void PlayEvasion()
		{
			ShakeHorizontal();
		}
	}
}
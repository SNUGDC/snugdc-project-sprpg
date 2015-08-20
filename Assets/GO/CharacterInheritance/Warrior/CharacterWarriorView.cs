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
				default:
					return;
			}
		}
	
		public void PlayAttack()
		{
			InstantiateFx (FxAttck);
		}

		public void PlayHeal()
		{
			InstantiateFx (FxHeal);
		}

		public void PlayStrongAttack()
		{
			InstantiateFx (FxStrongAttack);
		}
	}
}
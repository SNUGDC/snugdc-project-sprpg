using UnityEngine;
using Gem;

namespace SPRPG
{
	public class CharacterThiefView : CharacterView 
	{
		public GameObject FxStab;
		public GameObject FxSandAttack;
		public GameObject FxPoisonAttack;
		public GameObject FxAssasination;

		public override void PlaySkillStart(SkillBalanceData data)
		{
			switch (data.Key) 
			{
				case SkillKey.ThiefStab:
					PlayStab();
					return;
				case SkillKey.ThiefSandAttack:
					PlaySandAttack();
					return;
				case SkillKey.ThiefPoisonAttack:
					PlayPoisonAttack();
					return;
				case SkillKey.ThiefAssassination:
					PlayAssasination();
					return;
				default : 
					return;
			}

		}

		public void PlayStab()
		{
			InstantiateFx(FxStab);
		}

		public void PlaySandAttack()
		{
			InstantiateFx (FxSandAttack);
		}

		public void PlayPoisonAttack()
		{
			InstantiateFx (FxPoisonAttack);
		}

		public void PlayAssasination()
		{
			InstantiateFx (FxAssasination);
		}
	}
}
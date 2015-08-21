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
				case SkillKey.ThiefStab: PlayStab(); return;
				case SkillKey.ThiefSandAttack: PlaySandAttack(); return;
				case SkillKey.ThiefPoisonAttack: PlayPoisonAttack(); return;
				case SkillKey.ThiefAssassination: PlayAssasination(); return;
				default: Debug.LogError(LogMessages.EnumNotHandled(data.Key)); return;
			}

		}

		public void PlayStab()
		{
			Points.InstantiateOnAim(FxStab);
		}

		public void PlaySandAttack()
		{
			Points.InstantiateOnAim(FxSandAttack);
		}

		public void PlayPoisonAttack()
		{
			Points.InstantiateOnAim(FxPoisonAttack);
		}

		public void PlayAssasination()
		{
			Points.InstantiateOnAim(FxAssasination);
		}
	}
}
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
				case SkillKey.ThiefEvade: PlayEvade(); return;
				case SkillKey.ThiefSandAttack: PlaySandAttack(); return;
				case SkillKey.ThiefPoisonAttack: PlayPoisonAttack(); return;
				case SkillKey.ThiefAssassination: PlayAssasination(); return;
				default: Debug.LogError(LogMessages.EnumNotHandled(data.Key)); return;
			}

		}

		public void PlayStab()
		{
			Attack();
			Points.InstantiateOnAim(FxStab);
		}

		public void PlayEvade()
		{
			ShakeHorizontal();
		}

		public void PlaySandAttack()
		{
			Attack();
			Points.InstantiateOnAim(FxSandAttack);
		}

		public void PlayPoisonAttack()
		{
			Attack();
			Points.InstantiateOnAim(FxPoisonAttack);
		}

		public void PlayAssasination()
		{
			StrongAttack();
			Points.InstantiateOnAim(FxAssasination);
		}
	}
}
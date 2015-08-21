using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterMonkView : CharacterView
	{
		public GameObject FxHeal;
		public GameObject FxSacrifice;
		public GameObject FxEquility;
		public GameObject FxRevenge;
		public GameObject FxRecovery;

		public override void PlaySkillStart(SkillBalanceData data)
		{
			switch (data.Key) 
			{
				case(SkillKey.MonkHeal): PlayHeal(); return;
				case(SkillKey.MonkSacrifice): PlaySacrifice(); return;
				case(SkillKey.MonkEquility): PlayEquility(); return;
				case(SkillKey.MonkRevenge): PlayRevenge(); return;
				case(SkillKey.MonkRecovery): PlayRecovery(); return;
				default: Debug.LogError(LogMessages.EnumNotHandled(data.Key)); return;
			}
		}

		public void PlayHeal()
		{
			Points.InstantiateOnAim(FxHeal);
		}

		public void PlaySacrifice()
		{
			Points.InstantiateOnAim(FxSacrifice);
		}

		public void PlayEquility()
		{
			Points.InstantiateOnAim(FxEquility);
		}

		public void PlayRevenge()
		{
			Points.InstantiateOnAim(FxRevenge);
		}

		public void PlayRecovery()
		{
			Points.InstantiateOnAim(FxRecovery);
		}
	}
}
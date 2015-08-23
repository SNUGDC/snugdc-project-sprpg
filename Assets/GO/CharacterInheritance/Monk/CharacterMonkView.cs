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

		public override void PlaySkillStart(SkillBalanceData data, object argument)
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
			Buff();
			Points.InstantiateOnBuffTop(FxHeal);
		}

		public void PlaySacrifice()
		{
			Buff();
			Points.InstantiateOnCenter(FxSacrifice);
		}

		public void PlayEquility()
		{
			Buff();
			Points.InstantiateOnCenter(FxEquility);
		}

		public void PlayRevenge()
		{
			// todo: instantiate on boss
			Attack();
			Points.InstantiateOnAim(FxRevenge);
		}

		public void PlayRecovery()
		{
			Buff();
			Points.InstantiateOnBuffTop(FxRecovery);
		}
	}
}
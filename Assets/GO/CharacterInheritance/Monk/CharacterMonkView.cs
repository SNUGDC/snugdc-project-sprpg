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
				case(SkillKey.MonkHeal):
					PlayHeal();
					return;
				case(SkillKey.MonkSacrifice):
					PlaySacrifice();
					return;
				case(SkillKey.MonkEquility):
					PlayEquility();
					return;
				case(SkillKey.MonkRevenge):
					PlayRevenge();
					return;
				case(SkillKey.MonkRecovery):
					PlayRecovery();
					return;
				default:
					return;
			}
		}

		public void PlayHeal()
		{
			InstantiateFx (FxHeal);
		}

		public void PlaySacrifice()
		{
			InstantiateFx (FxSacrifice);
		}

		public void PlayEquility()
		{
			InstantiateFx (FxEquility);
		}

		public void PlayRevenge()
		{
			InstantiateFx (FxRevenge);
		}

		public void PlayRecovery()
		{
			InstantiateFx (FxRecovery);
		}
	}
}
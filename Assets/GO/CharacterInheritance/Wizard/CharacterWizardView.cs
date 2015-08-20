using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterWizardView : CharacterView
	{
		public GameObject FxFireBolt;
		public GameObject FxIceBolt;
		public GameObject FxLigtheningBolt;
		public GameObject FxFireBall;
		public GameObject FxIceSpear;
		public GameObject FxLigthening;

		public override void PlaySkillStart(SkillBalanceData data)
		{
			Attack();

			switch (data.Key)
			{
				case SkillKey.WizardFireBolt:
					PlayFireBolt();
					return;
				case SkillKey.WizardIceBolt:
					PlayIceBolt();
					return;
				case SkillKey.WizardLighteningBolt:
					PlayLighteningBolt();
					return;
				case SkillKey.WizardFireBall:
					PlayFireBall();
					return;
				case SkillKey.WizardIceSpear:
					PlayIceSpear();
					return;
				case SkillKey.WizardLightening:
					PlayLightening();
					return;
				default:
					return;
			}
		}

		public void PlayFireBolt()
		{
			InstantiateFx (FxFireBolt);
		}

		public void PlayIceBolt()
		{
			InstantiateFx (FxIceBolt);
		}

		public void PlayLighteningBolt()
		{
			InstantiateFx (FxLigtheningBolt);
		}

		public void PlayFireBall()
		{
			InstantiateFx (FxFireBall);
		}

		public void PlayIceSpear()
		{
			InstantiateFx (FxIceSpear);
		}

		public void PlayLightening()
		{
			InstantiateFx (FxLigthening);
		}
	}
}
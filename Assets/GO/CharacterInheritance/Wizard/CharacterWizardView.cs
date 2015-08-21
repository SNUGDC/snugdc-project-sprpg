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
				case SkillKey.WizardFireBolt: PlayFireBolt(); return;
				case SkillKey.WizardIceBolt: PlayIceBolt(); return;
				case SkillKey.WizardLighteningBolt: PlayLighteningBolt(); return;
				case SkillKey.WizardFireBall: PlayFireBall(); return;
				case SkillKey.WizardIceSpear: PlayIceSpear(); return;
				case SkillKey.WizardLightening: PlayLightening(); return;
				default: Debug.LogError(LogMessages.EnumNotHandled(data.Key)); return;
			}
		}

		public void PlayFireBolt()
		{
			Points.InstantiateOnAim(FxFireBolt);
		}

		public void PlayIceBolt()
		{
			Points.InstantiateOnAim(FxIceBolt);
		}

		public void PlayLighteningBolt()
		{
			Points.InstantiateOnAim(FxLigtheningBolt);
		}

		public void PlayFireBall()
		{
			Points.InstantiateOnAim(FxFireBall);
		}

		public void PlayIceSpear()
		{
			Points.InstantiateOnAim(FxIceSpear);
		}

		public void PlayLightening()
		{
			Points.InstantiateOnAim(FxLigthening);
		}
	}
}
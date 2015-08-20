using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterWizardView : CharacterView
	{
		public GameObject FxFireBolt;

		public override void PlaySkillStart(SkillBalanceData data)
		{
			Attack();

			switch (data.Key)
			{
				case SkillKey.WizardFireBolt:
					PlayFireBoltFx();
					return;
				default:
					return;
			}
		}

		public void PlayFireBoltFx()
		{
			var fx = FxFireBolt.Instantiate();
			fx.transform.SetParent(FxRoot, false);
		}
	}
}
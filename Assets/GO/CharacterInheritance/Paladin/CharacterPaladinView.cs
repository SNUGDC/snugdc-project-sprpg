using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterPaladinView : CharacterView 
	{
		public GameObject FxMassHeal;

		public override void PlaySkillStart(SkillBalanceData data)
		{
			switch (data.Key)
			{
				case SkillKey.PaladinMassHeal:
					PlayMassHeal();
					return;
				default:
					return;
			}
		}

		public void PlayMassHeal()
		{
			var fx = FxMassHeal.Instantiate();
			fx.transform.SetParent(FxRoot, false);
		}
	}
}
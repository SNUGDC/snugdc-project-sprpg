using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterPaladinView : CharacterView 
	{
		public GameObject FxMassHeal;
		public GameObject FxBash;
		public GameObject FxDefense;
		public GameObject FxPurification;
		public GameObject FxBigMassHeal;
		public GameObject FxMassDefense;

		public override void PlaySkillStart(SkillBalanceData data)
		{
			switch (data.Key)
			{
				case SkillKey.PaladinMassHeal:
					PlayMassHeal();
					return;
				case SkillKey.PaladinBash:
					PlayBash();
					return;
				case SkillKey.PaladinDefense:
					PlayDefense();
					return;
				case SkillKey.PaladinPurification:
					PlayPurification();
					return;
				case SkillKey.PaladinBigMassHeal:
					PlayBigMassHeal();
					return;
				case SkillKey.PaladinMassDefense:
					PlayMassDefense();
					return;

				default:
					return;
			}
		}

		public void InstantiateFx(GameObject fxPrefab)
		{
			var fx = fxPrefab.Instantiate ();
			fx.transform.SetParent (FxRoot, false);
		}

		public void PlayMassHeal()
		{
			InstantiateFx (FxMassHeal);
		}

		public void PlayBash()
		{
			InstantiateFx (FxBash);		
		}

		public void PlayDefense()
		{
			InstantiateFx (FxDefense);		
		}

		public void PlayPurification()
		{
			InstantiateFx (FxPurification);
		}

		public void PlayBigMassHeal()
		{
			InstantiateFx (FxBigMassHeal);
		}

		public void PlayMassDefense()
		{
			InstantiateFx (FxMassDefense);
		}
	}
}
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
				case SkillKey.PaladinMassHeal: PlayMassHeal(); return;
				case SkillKey.PaladinBash: PlayBash(); return;
				case SkillKey.PaladinDefense: PlayDefense(); return;
				case SkillKey.PaladinPurification: PlayPurification(); return;
				case SkillKey.PaladinBigMassHeal: PlayBigMassHeal(); return;
				case SkillKey.PaladinMassDefense: PlayMassDefense(); return;
				default: Debug.LogError(LogMessages.EnumNotHandled(data.Key)); return;
			}
		}

		public void PlayMassHeal()
		{
			Points.InstantiateOnAim(FxMassHeal);
		}

		public void PlayBash()
		{
			Points.InstantiateOnAim(FxBash);		
		}

		public void PlayDefense()
		{
			Points.InstantiateOnAim(FxDefense);		
		}

		public void PlayPurification()
		{
			Points.InstantiateOnAim(FxPurification);
		}

		public void PlayBigMassHeal()
		{
			Points.InstantiateOnAim(FxBigMassHeal);
		}

		public void PlayMassDefense()
		{
			Points.InstantiateOnAim(FxMassDefense);
		}
	}
}
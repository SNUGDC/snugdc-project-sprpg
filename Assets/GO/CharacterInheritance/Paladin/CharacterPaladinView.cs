using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterPaladinView : CharacterView 
	{
		public GameObject FxMassHeal;
		public GameObject FxBash;
		public GameObject FxPurification;
		public GameObject FxBigMassHeal;

		public override void PlaySkillStart(SkillBalanceData data, object argument)
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

		private void PlayAnimationDefense()
		{
			Animator.SetTrigger("PaladinDefense");
		}

		public void PlayAnimationDefenseRelease()
		{
			Animator.SetTrigger("PaladinDefenseRelease");
		}

		public void PlayMassHeal()
		{
			Buff();
			Points.InstantiateOnBuffTop(FxMassHeal);
		}

		public void PlayBash()
		{
			Attack();
			Points.InstantiateOnAim(FxBash);		
		}

		public void PlayDefense()
		{
			PlayAnimationDefense();
		}

		public void PlayPurification()
		{
			Points.InstantiateOnBottom(FxPurification);
		}

		public void PlayBigMassHeal()
		{
			Buff();
			Points.InstantiateOnBuffTop(FxBigMassHeal);
		}

		public void PlayMassDefense()
		{
			PlayAnimationDefense();
		}
	}
}
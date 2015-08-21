using System;
using Gem;
using SPRPG.Battle.View;
using UnityEngine;

namespace SPRPG
{
	public class CharacterArcherView : CharacterView
	{
		public GameObject FxReload;
		public GameObject FxEvadeShot;
		public GameObject FxSnipe;
		public GameObject FxWeakPointAttack;
		public GameObject FxArrowRain;

		public override void PlaySkillStart(SkillBalanceData data)
		{
			switch (data.Key)
			{
				case SkillKey.ArcherReload: PlayReload(); break;
				case SkillKey.ArcherEvadeShot: PlayEvadeShot(); break;
				case SkillKey.ArcherSnipe: PlaySnipe(); break;
				case SkillKey.ArcherWeakPointAttack: PlayWeakPointAttack(); break;
				case SkillKey.ArcherArrowRain: PlayArrowRain(); break;
				default:
					Debug.LogError(LogMessages.EnumNotHandled(data.Key));
					return;
			}
		}

		public void PlayReload()
		{
			Animator.SetTrigger("ArcherReload");
			Points.InstantiateOnBuffTop(FxReload);
		}

		public void PlayEvadeShot()
		{
			Attack();
			Points.InstantiateOnAim(FxEvadeShot);
		}

		public void PlaySnipe()
		{
			Attack();
			Points.InstantiateOnAim(FxSnipe);
		}

		public void PlayWeakPointAttack()
		{
			Attack();
			Points.InstantiateOnAim(FxWeakPointAttack);
		}

		public void PlayArrowRain()
		{
			StrongAttack();
			Points.InstantiateOnAim(FxArrowRain);
		}
	}
}
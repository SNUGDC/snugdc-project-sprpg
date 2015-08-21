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
			// todo
		}

		public void PlayReload()
		{
			Points.InstantiateOnAim(FxReload);
		}

		public void PlayEvadeShot()
		{
			Points.InstantiateOnAim(FxEvadeShot);
		}

		public void PlaySnipe()
		{
			Points.InstantiateOnAim(FxSnipe);
		}

		public void PlayWeakPointAttack()
		{
			Points.InstantiateOnAim(FxWeakPointAttack);
		}

		public void PlayArrowRain()
		{
			Points.InstantiateOnAim(FxArrowRain);
		}
	}
}
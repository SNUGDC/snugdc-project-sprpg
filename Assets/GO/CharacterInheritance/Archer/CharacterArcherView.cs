using Gem;
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
			}
		}

		public void PlayReload()
		{
			InstantiateFx (FxReload);
		}

		public void PlayEvadeShot()
		{
			InstantiateFx (FxEvadeShot);
		}

		public void PlaySnipe()
		{
			InstantiateFx (FxSnipe);
		}

		public void PlayWeakPointAttack()
		{
			InstantiateFx (FxWeakPointAttack);
		}

		public void PlayArrowRain()
		{
			InstantiateFx (FxArrowRain);
		}
	}
}
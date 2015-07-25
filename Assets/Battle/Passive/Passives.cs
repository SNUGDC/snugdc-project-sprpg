using UnityEngine;

namespace SPRPG.Battle
{
	public class ArcherPassive : Passive
	{
		private const Tick RechargeCooltime = Const.Term;

		public int Arrows { get; private set; }
		
		public bool IsArrowLeft { get { return Arrows > 0; } }

		private Tick _rechargeTickLeft = RechargeCooltime;

		public override void Tick()
		{
			if (--_rechargeTickLeft > 0)
				return;

			_rechargeTickLeft = RechargeCooltime;
			++Arrows;
		}

		public ArcherPassive()
		{
			Arrows = 3;
		}


		public void DecreaseArrow()
		{
			if (Arrows > 0)
				--Arrows;
			else
			{
				Debug.LogError("No more arrows!");
			}
		}

		public bool RemoveAllArrows()
		{
			var ret = Arrows > 0;
			Arrows = 0;
			return ret;
		}

#if UNITY_EDITOR
		public override void OnInspectorGUI()
		{
			GUILayout.Label("arrow left: " + Arrows);
			GUILayout.Label("arrow tick left: " + _rechargeTickLeft);
		}
#endif
	}
}

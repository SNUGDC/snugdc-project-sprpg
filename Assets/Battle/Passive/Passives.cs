using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class ArcherPassive : Passive
	{
		public int Arrows { get; private set; }
		
		public bool IsArrowLeft { get { return Arrows > 0; } }

		private readonly Tick RechargeCooltime;
		private Tick _rechargeTickLeft;

		public ArcherPassive()
		{
			Arrows = 3;
			var _data = CharacterBalance._.Find(CharacterId.Archer).Special.ToObject<ArcherSpecialBalanceData>();
			RechargeCooltime = _data.ArrowRechargeCooltime;
			_rechargeTickLeft = RechargeCooltime;
		}

		public override void Tick()
		{
			if (--_rechargeTickLeft > 0)
				return;

			_rechargeTickLeft = RechargeCooltime;
			++Arrows;
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

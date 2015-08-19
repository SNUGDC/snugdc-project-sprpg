using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class ArcherPassive : Passive
	{
		public int Arrows { get; private set; }
		public bool IsArrowLeft { get { return Arrows > 0; } }
		public bool IsArrowMax { get { return Arrows >= _data.ArrowMax; } }

		private readonly ArcherSpecialBalanceData _data;
		private readonly Tick _rechargeCooltime;
		private Tick _rechargeTickLeft;

		public ArcherPassive()
		{
			_data = CharacterBalance._.Find(CharacterId.Archer).Special.ToObject<ArcherSpecialBalanceData>();
			Arrows = _data.ArrowInitial;
			_rechargeCooltime = _data.ArrowRechargeCooltime;
			_rechargeTickLeft = _rechargeCooltime;
		}

		public override void Tick()
		{
			if (IsArrowMax) return;
			if (--_rechargeTickLeft > 0) return;
			_rechargeTickLeft = _rechargeCooltime;
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

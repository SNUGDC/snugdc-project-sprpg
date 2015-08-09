using System.Collections.Generic;
using UnityEngine;

namespace SPRPG.Battle
{
	public class BattleBalanceData
	{
		public Tick TickPerSecond;
		public Tick InputValidBefore;
		public Tick InputValidAfter;
		public float PartyPlaceLerp;
		public List<Vector3> PartyPositions;
	}

	public sealed class BattleBalance : Balance<BattleBalanceData>
	{
		public static readonly BattleBalance _ = new BattleBalance();

		public BattleBalance() : base("battle")
		{}
	}
}

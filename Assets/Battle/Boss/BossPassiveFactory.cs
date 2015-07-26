using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public static class BossWholePassiveFactory
	{
		public static BossPassiveFactory Map(BossId id)
		{
			switch (id)
			{
				case BossId.Radiation: return BossRadiationPassiveFactory._;
				default:
					Debug.LogError(LogMessages.EnumNotHandled(id));
					return null;
			}
		}
	}

	public abstract class BossPassiveFactory
	{
		public abstract BossPassive Create(Battle context, Boss boss, BossPassiveBalanceData data);
	}
}

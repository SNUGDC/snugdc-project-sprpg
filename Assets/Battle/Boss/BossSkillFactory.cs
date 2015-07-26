using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public abstract class BossSkillFactory
	{
		public abstract BossSkillActor Create(Battle context, Boss boss, BossSkillBalanceData data);
	}

	public static class BossWholeSkillFactory
	{
		public static BossSkillFactory Map(Boss boss)
		{
			switch (boss.Id)
			{
				case BossId.Radiation:
					return BossRadiationSkillFactory._;
				default:
					Debug.LogError(LogMessages.EnumUndefined(boss.Id));
					return null;
			}
		}
	}
}

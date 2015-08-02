using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public abstract class BossSkillFactory
	{
		public abstract BossSkillActor Create(BossSkillBalanceData data, Battle context, Boss owner);
	}

	public static class BossWholeSkillFactory
	{
		public static BossSkillFactory Map(Boss owner)
		{
			switch (owner.Id)
			{
				case BossId.Radiation:
					return BossRadiationSkillFactory._;
				default:
					Debug.LogError(LogMessages.EnumUndefined(owner.Id));
					return null;
			}
		}
	}
}

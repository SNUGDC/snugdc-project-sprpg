using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public static class BossSkillFactory
	{
		public static BossSkillActor Create(Battle context, Boss boss, BossSkillBalanceData data)
		{
			switch (boss.Id)
			{
				case BossId.Radiation:
					return CreateRadiation(context, boss, data);
				default:
					Debug.LogError(LogMessages.EnumUndefined(boss.Id));
					return null;
			}
		}

		public static BossSkillActor CreateRadiation(Battle context, Boss boss, BossSkillBalanceData data)
		{
			Debug.Assert(boss.Id == BossId.Radiation);

			switch (data.Key)
			{
				case BossSkillLocalKey.Attack:
					return new RadiationAttackSkillActor(context, boss, data);
				default:
					Debug.LogError(LogMessages.EnumUndefined(data.Key));
					return null;

			}
		}
	}
}

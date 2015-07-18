using System;
using Gem;

namespace SPRPG.Battle
{
	public class Boss
	{
		public BossId Id { get { return Data.Id; } }
		public readonly BossBalanceData Data;

		public bool IsAlive { get { return Hp > 0; } }

		public Hp Hp { get; private set; }
		private readonly Hp _hpMax;

		public Action<Boss> OnDead;

		public Boss(BossBalanceData data)
		{
			Data = data;
			Hp = _hpMax = Data.Stats.Hp.ToValue();
		}

		public void Hit(Damage val)
		{
			Hp -= val.Value;
			if (Hp < 0)
				OnDead.CheckAndCall(this);
		}
	}

	public static class BossHelper
	{
		public static BossSkillBalanceData Find(this Boss thiz, BossSkillLocalKey key)
		{
			BossSkillBalanceData ret;
			thiz.Data.Skills.TryGet(key, out ret);
			return ret;
		}
	}
}
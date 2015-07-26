using System;
using Gem;

namespace SPRPG.Battle
{
	public class Boss
	{
		public BossId Id { get { return Data.Id; } }
		private readonly Battle _context;
		public readonly BossBalanceData Data;
		private readonly BossPassiveManager _passiveManager;

		public bool IsAlive { get { return Hp > 0; } }

		private Hp _hp;
		public Hp Hp
		{
			get { return _hp; }
			private set
			{
				value = (Hp)((int)value).Clamp(0, (int)_hpMax);
				if (Hp == value) return;
				var oldHp = Hp;
				_hp = value;
				Events.OnBossHpChanged.CheckAndCall(this, oldHp);
			}
		}
		private readonly Hp _hpMax;

		public Action<Boss> OnDead;

		public Boss(Battle context, BossBalanceData data)
		{
			_context = context;
			Data = data;
			Hp = _hpMax = Data.Stats.Hp.ToValue();
			_passiveManager = new BossPassiveManager(context, this);
		}

		public void TickPassive()
		{
			_passiveManager.Tick();
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
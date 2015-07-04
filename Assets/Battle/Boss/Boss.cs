using System;
using Gem;

namespace SPRPG.Battle
{
	public class Boss
	{
		public Hp Hp { get; private set; }
		private readonly Hp _hpMax;

		public Action<Boss> OnDead;

		public Boss(Hp hpMax)
		{
			Hp = _hpMax = hpMax;
		}

		public void Hit(Damage val)
		{
			Hp -= val.Value;
			if (Hp < 0)
				OnDead.CheckAndCall(this);
		}
	}
}
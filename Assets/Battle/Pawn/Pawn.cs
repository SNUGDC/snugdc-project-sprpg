﻿using System;
using Gem;

namespace SPRPG.Battle
{
	public class Pawn
	{
		public bool IsAlive { get { return Hp > 0; } }
		private Hp _hp;
		public Hp Hp
		{
			get { return _hp; }
			private set
			{
				value = (Hp)((int)value).Clamp(0, (int)HpMax);
				if (_hp == value) return;
				var oldHp = _hp;
				_hp = value;
				AfterHpChanged(oldHp);
			}
		}
		public readonly Hp HpMax;

		protected Pawn(Stats stats)
		{
			Hp = HpMax = stats.Hp.ToValue();
		}

		public virtual void Hit(Damage dmg)
		{
			Hp -= dmg.Value;
		}

		public virtual void Heal(Hp val)
		{
			Hp += (int)val;
		}

		protected virtual void AfterHpChanged(Hp old) {}
	}

	public class Pawn<T> : Pawn where T : Pawn<T>
	{
		public Action<T, Hp, Hp> OnHpChanged;
		public Action<T> OnDead;

		protected Pawn(Stats stats) : base(stats)
		{ }

		protected override void AfterHpChanged(Hp old)
		{
			if (OnHpChanged != null)
				OnHpChanged((T) this, Hp, old);

			if (Hp == 0 && OnDead != null)
				OnDead((T) this);
		}
	}
}

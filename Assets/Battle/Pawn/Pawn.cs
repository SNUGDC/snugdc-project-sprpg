using System;
using Gem;

namespace SPRPG.Battle
{
	public class Pawn<T> where T : Pawn<T>
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

				AfterHpChanged();

				if (OnHpChanged != null) 
					OnHpChanged((T)this, _hp, oldHp);

				if (value == 0 && OnDead != null)
					OnDead((T)this);
			}
		}

		public readonly Hp HpMax;

		public Action<T, Hp, Hp> OnHpChanged;
		public Action<T> OnDead;

		public Pawn(Stats stats)
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

		protected virtual void AfterHpChanged()
		{}
	}
}

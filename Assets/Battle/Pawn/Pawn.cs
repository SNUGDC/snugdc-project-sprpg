using System;
using Gem;

namespace SPRPG.Battle
{
	public enum PawnInvincibleKey { }

	public class Pawn
	{
		public bool IsAlive { get { return Hp > 0; } }
		public Stats Stats { get; private set; }

		private Hp _hp;
		public Hp HpMax { get { return Stats.Hp.ToValue(); } }
		public Hp Hp
		{
			get { return _hp; }
			private set
			{
				value = (Hp)((int)value).Clamp(0, (int)HpMax);
				if (_hp == value) return;
				SetHpForced(value);
			}
		}

		public Percentage HpPercentage { get { return (Percentage)((int)Hp/(float) (int) HpMax*100); } }

		public StatusCondition StatusCondition { get; private set; }
		private readonly SetBool<PawnInvincibleKey> _invincible = new SetBool<PawnInvincibleKey>();
		public bool IsInvincible { get { return _invincible; } }

		protected Pawn(Stats stats)
		{
			Stats = stats;
			Hp = HpMax;
		}

		public virtual void AfterTurn()
		{
			if (StatusCondition != null)
				TickStatusCondition();
		}

		public void TickStatusCondition()
		{
			StatusCondition.Tick();
			if (!StatusCondition.IsActive)
				StatusCondition = null;
		}

		public Damage ApplyModifier(Damage damage)
		{
			return Stats.DamageModifier.Apply(damage);
		}

		public bool SetInvincible(PawnInvincibleKey key) { return _invincible.Add(key); }
		public bool UnsetInvincible(PawnInvincibleKey key) { return _invincible.Remove(key); }
		public PawnInvincibleKey SetInvincible()
		{
			var key = (PawnInvincibleKey) Battle.KeyGen.Next();
			SetInvincible(key);
			return key;
		}

		public void SetHpForced(Hp val)
		{
			var oldHp = _hp;
			_hp = val;
			AfterHpChanged(oldHp);
		}

		public void Attack(Pawn target, Damage damage)
		{
			var damageModified = ApplyModifier(damage);
			target.Hit(damageModified);
		}

		public virtual void Hit(Damage dmg)
		{
			if (_invincible) return;
			Hp -= dmg.Value;
		}

		public virtual void Heal(Hp val)
		{
			Hp += (int)val;
		}

		public bool TestAndGrant(Percentage percentage, StatusConditionType type, Tick duration)
		{
			if (!percentage.Test())
				return false;

			if (StatusCondition != null)
			{
				if (StatusCondition.Type == type)
				{
					StatusCondition.Reserve(duration);
					return true;
				}
			}

			StatusCondition = StatusConditionFactory.Create(this, type, duration);
			return true;
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

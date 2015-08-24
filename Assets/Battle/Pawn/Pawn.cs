using System;
using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public enum PawnInvincibleKey { }

	public abstract class Pawn
	{
		public bool IsAlive { get { return Hp > 0; } }
		public bool IsDead { get { return !IsAlive; } }
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

		public readonly Dictionary<StatusConditionType, StatusCondition> StatusConditions = new Dictionary<StatusConditionType, StatusCondition>();
		public abstract StatusConditionGroup StatusConditionGroup { get; }
		public bool HasSomeStatusCondition { get { return !StatusConditions.Empty(); } }
		public bool HasStatusCondition(StatusConditionType type) { return StatusConditions.ContainsKey(type); }
		public bool IsFreezed { get { return HasStatusCondition(StatusConditionType.Freeze); } }
		public bool IsPoisoned { get { return HasStatusCondition(StatusConditionType.Poison); } }
		public bool IsBlind { get { return HasStatusCondition(StatusConditionType.Blind); } }
		
		private readonly SetBool<PawnInvincibleKey> _invincible = new SetBool<PawnInvincibleKey>();
		public bool IsInvincible { get { return _invincible; } }

		protected Pawn(Stats stats)
		{
			Stats = stats;
			Hp = HpMax;
		}

		public virtual void AfterTurn()
		{
			TickStatusCondition();
		}

		public void TickStatusCondition()
		{
			var markForRemove = new List<StatusConditionType>(StatusConditions.Count);
			foreach (var kv in StatusConditions)
			{
				var statusCondition = kv.Value;
				statusCondition.Tick();
				if (!statusCondition.IsActive)
					markForRemove.Add(statusCondition.Type);
			}

			foreach (var typeToRemove in markForRemove)
				StopStatusCondition(typeToRemove);
		}

		public Damage ApplyModifier(Damage damage)
		{
			return Stats.DamageModifier.Apply(damage);
		}

		public bool SetInvincible(PawnInvincibleKey key) { return _invincible.Add(key); }
		public bool UnsetInvincible(PawnInvincibleKey key) { return _invincible.Remove(key); }
		public bool HasInvincible(PawnInvincibleKey key) { return _invincible.Contains(key); }
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

		protected virtual void AfterHpChanged(Hp old) {}

		public void Attack(Pawn target, Damage damage)
		{
			var damageModified = ApplyModifier(damage);
			target.Hit(damageModified);
			AfterAttack(target);
		}

		protected virtual void AfterAttack(Pawn target) { }

		public virtual void Hit(Damage damage)
		{
			if (_invincible) return;
			Hp -= damage.Value;
			AfterHit(damage);
		}

		protected virtual void AfterHit(Damage damage) { }

		public void MissHit()
		{
			AfterMissHit();
		}

		protected virtual void AfterMissHit() { } 

		public virtual void Heal(Hp val)
		{
			if (IsDead)
			{
				Debug.LogWarning("heal but dead.");
				return;
			}
			Hp += (int)val;
			AfterHeal(val);
		}

		protected virtual void AfterHeal(Hp val) { }

		public bool TestAndGrant(StatusConditionTest test)
		{
			var statusConditionData = BattleBalance._.Data.GetStatusCondition(StatusConditionGroup, test.Type);
			return TestAndGrant(test, statusConditionData.DefaultDuration);
		}

		public bool TestAndGrant(StatusConditionTest test, Tick duration)
		{
			if (!test.Percentage.Test())
				return false;

			StatusCondition statusCondition;
			if (StatusConditions.TryGetValue(test.Type, out statusCondition))
			{
				statusCondition.Reserve(duration);
				return true;
			}

			statusCondition = StatusConditionFactory.Create(this, test.Type, duration);
			StatusConditions.Add(test.Type, statusCondition);
			AfterGrantStatusCondition(test.Type);
			return true;
		}

		protected virtual void AfterGrantStatusCondition(StatusConditionType type) { }

		public bool TryCure(StatusConditionType type)
		{
			return StopStatusCondition(type);
		}

		public void CureAll()
		{
			var statusConditions = new Dictionary<StatusConditionType, StatusCondition>(StatusConditions);
			foreach (var kv in StatusConditions)
				StopStatusCondition(kv.Key);
		}

		private bool StopStatusCondition(StatusConditionType type)
		{
			var ret = StatusConditions.TryRemove(type);
			AfterStopStatusCondition(type);
			return ret;
		}

		protected virtual void AfterStopStatusCondition(StatusConditionType type) { }

		public bool TestAndGrant(StunTest stunTest)
		{
			if (!stunTest.Percentage.Test())
				return false;
			Stun();
			return true;
		}

		protected abstract void Stun();
	}

	public abstract class Pawn<T> : Pawn where T : Pawn<T>
	{
		public Action<T, Hp, Hp> OnHpChanged;
		public Action<T> OnDead;
		public Action<T, Hp> OnAfterHeal;
		public Action<T, Damage> OnAfterHit;
		public Action<T> OnMissHit;
		public Action<T, Pawn> OnAfterAttack;
		public Action<T, StatusConditionType> OnGrantStatusCondition;
		public Action<T, StatusConditionType> OnStopStatusCondition;

		protected Pawn(Stats stats) : base(stats)
		{ }

		protected override void AfterHpChanged(Hp old)
		{
			OnHpChanged.CheckAndCall((T) this, Hp, old);
			if (Hp == 0) OnDead.CheckAndCall((T) this);
		}

		protected override void AfterHeal(Hp val)
		{
			OnAfterHeal.CheckAndCall((T)this, val);
		}

		protected override void AfterHit(Damage damage)
		{
			OnAfterHit.CheckAndCall((T)this, damage);	
		}

		protected override void AfterMissHit()
		{
			OnMissHit.CheckAndCall((T)this);
		}

		protected override void AfterAttack(Pawn target)
		{
			OnAfterAttack.CheckAndCall((T) this, target);
		}

		protected override void AfterGrantStatusCondition(StatusConditionType type)
		{
			OnGrantStatusCondition.CheckAndCall((T) this, type);
		}

		protected override void AfterStopStatusCondition(StatusConditionType type)
		{
			OnStopStatusCondition.CheckAndCall((T) this, type);
		}
	}
}

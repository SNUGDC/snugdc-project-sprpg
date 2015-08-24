using System;
using Gem;

namespace SPRPG.Battle
{
	[Serializable]
	public struct CharacterDef
	{
		public CharacterId Id { get { return Data.Id; } }
		public CharacterBalanceData Data;
		public SkillSet SkillSet;

		public CharacterDef(CharacterBalanceData data, SkillSet skillSet)
		{
			Data = data;
			SkillSet = skillSet;
		}

		public SkillSet GetSkillSetOrDefault()
		{
			return SkillSet ?? Data.SkillSetDefault;
		}
	}

	public class CharacterGuard
	{
		public bool IsEnabled { get; private set; }
		public Action<bool> OnChanged;

		public void Enable()
		{
			if (IsEnabled) return;
			IsEnabled = true;
			OnChanged.CheckAndCall(true);
		}

		public bool TryRelease()
		{
			if (!IsEnabled) return false;
			IsEnabled = false;
			OnChanged.CheckAndCall(false);
			return true;
		}
	}

	public class DamageIntercepter
	{
		public enum Key_ { }
		public delegate void Intercept(Character character, Damage damage);

		private static Key_ _uniqueKey;
		public static Key_ IssueUniqueKey() { return ++_uniqueKey; }

		public Key_ Key { get; private set; }
		private Intercept _intercepter;

		public bool HasValue { get { return Key == default(Key_); } }

		public void Set(Key_ key, Intercept intercepter)
		{
			Key = key;
			_intercepter = intercepter;
		}

		public bool ClearIfKeySame(Key_ key)
		{
			if (Key != key) return false;
			Key = default(Key_);
			_intercepter = null;
			return true;
		}

		public bool TryTake(Character character, Damage damage)
		{
			if (_intercepter == null) return false;
			_intercepter(character, damage);
			return true;
		}
	}

	public class Character : Pawn<Character>
	{
		public CharacterId Id { get { return Data.Id; } }
		private readonly Battle _context;
		public readonly CharacterBalanceData Data;

		public readonly Passive Passive;
		public readonly SkillManager SkillManager;

		private Tick _evadeDurationLeft;

		public override StatusConditionGroup StatusConditionGroup { get { return StatusConditionGroup.Character; } }

		public readonly CharacterGuard Guard = new CharacterGuard();
		public readonly DamageIntercepter DamageIntercepter = new DamageIntercepter();

		public Action<Character, SkillSlot, SkillActor> OnSkillStart;
		public Action<Character, Damage> OnEvade;
		public Action<Character> OnStun;

		public Character(CharacterDef def, Battle context)
			: base(def.Data.Stats)
		{
			_context = context;
			Data = def.Data;
			Passive = PassiveFactory.Create(Data.Passive, context, this);
			SkillManager = new SkillManager(def.SkillSet, context, this);
		}

		public void TickBeforeTurn()
		{
			if (CheckEvadeDuration())
				_evadeDurationLeft -= 1;
		}

		public void TickPassive()
		{
			if (IsDead) return;
			Passive.Tick();
		}

		public bool CheckEvadeDuration()
		{
			return _evadeDurationLeft > 0;
		}

		public override void Hit(Damage damage)
		{
			if (CheckEvadeDuration())
			{
				OnEvade.CheckAndCall(this, damage);
				return;
			}

			if (Guard.TryRelease()) return;
			if (DamageIntercepter.TryTake(this, damage)) return;
			base.Hit(damage);
		}

		public void Evade(Tick duration)
		{
			if (_evadeDurationLeft > duration) return;
			_evadeDurationLeft = duration;
		}

		public SkillActor TryPerformSkill(SkillSlot idx)
		{
			if (IsDead) return null;
			var ret = SkillManager.TryPerform(idx);

			var shouldReleaseGuard = true;
			if (ret != null) shouldReleaseGuard &= !ret.Data.PreserveGuard;
			if (shouldReleaseGuard) Guard.TryRelease();

			if (ret != null)
				OnSkillStart.CheckAndCall(this, idx, ret);

			return ret;
		}

		protected override void Stun()
		{
			OnStun.CheckAndCall(this);
		}
	}
}
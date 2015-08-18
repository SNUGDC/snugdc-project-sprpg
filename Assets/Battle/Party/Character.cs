using System;
using Gem;

namespace SPRPG.Battle
{
	public class Character : Pawn<Character>
	{
		public CharacterId Id { get { return Data.Id; } }
		private readonly Battle _context;
		public readonly CharacterData Data;

		public readonly Passive Passive;
		public readonly SkillManager SkillManager;

		private Tick _evadeDurationLeft;

		public override StatusConditionGroup StatusConditionGroup { get { return StatusConditionGroup.Character; } }

		public Action<Character, SkillSlot, SkillActor> OnSkillStart;
		public Action<Character> OnStun;

		public Character(CharacterData data, Battle context)
			: base(data.Stats)
		{
			_context = context;
			Data = data;
			Passive = PassiveFactory.Create(data.Passive);
			SkillManager = new SkillManager(data.SkillSet, context, this);
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

		public override void Hit(Damage dmg)
		{
			if (CheckEvadeDuration())
				return;
			base.Hit(dmg);
		}

		public void Evade(Tick duration)
		{
			_evadeDurationLeft = duration;
		}

		public SkillActor TryPerformSkill(SkillSlot idx)
		{
			if (IsDead) return null;
			var ret = SkillManager.TryPerform(idx);
			if (ret != null) OnSkillStart.CheckAndCall(this, idx, ret);
			return ret;
		}

		protected override void Stun()
		{
			OnStun.CheckAndCall(this);
		}
	}
}
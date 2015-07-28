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

		public Character(CharacterData data, Battle context)
			: base(data.Stats)
		{
			_context = context;
			Data = data;
			Passive = PassiveFactory.Create(data.Passive);
			SkillManager = new SkillManager(data.SkillSet, context, this);
		}

		public void BeforeTurn()
		{
			if (CheckEvadeDuration())
				_evadeDurationLeft -= 1;
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

		public void PerformSkill(SkillSlot idx)
		{
			SkillManager.Perform(idx);
		}
	}
}
namespace SPRPG.Battle
{
	public class Character : Pawn<Character>
	{
		public CharacterId Id { get { return Data.Id; } }
		private readonly CharacterData _data;
		public CharacterData Data { get { return _data; } }

		public readonly Passive Passive;
		private readonly SkillManager _skillManager;
		public SkillManager SkillManager { get { return _skillManager; } }

		private Tick _evadeDurationLeft;

		public Character(CharacterData data)
			: base(data.Stats)
		{
			_data = data;
			Passive = PassiveFactory.Create(data.Passive);
			_skillManager = new SkillManager(data.SkillSet, this);
		}

		public void BeforeTurn()
		{
			if (CheckEvadeDuration())
				_evadeDurationLeft -= 1;
		}

		public void AfterTurn()
		{
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
			_skillManager.Perform(idx);
		}
	}
}
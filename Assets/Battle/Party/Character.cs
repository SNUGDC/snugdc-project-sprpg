namespace SPRPG.Battle
{
	public class Character
	{
		public CharacterId Id { get { return Data.Id; } }

		private readonly CharacterData _data;
		public CharacterData Data { get { return _data; } }

		public bool IsAlive { get { return Hp > 0; } }

		public Hp Hp { get; private set; }
		public readonly Hp HpMax;

		private readonly Passive _passive;

		private readonly SkillManager _skillManager;
		public SkillManager SkillManager { get { return _skillManager; } }

		private Tick _evadeDurationLeft;

		public Character(CharacterData data)
		{
			_data = data;
			Hp = HpMax = data.Stats.Hp.ToValue();
			_passive = PassiveFactory.Create(data.Passive);
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

		public void Hit(Damage dmg)
		{
			if (CheckEvadeDuration())
				return;
			Hp -= dmg.Value;
		}

		public void Heal(Hp val)
		{
			Hp += (int)val;
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
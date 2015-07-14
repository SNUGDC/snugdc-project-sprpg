namespace SPRPG.Battle
{
	public class Character
	{
		public CharacterId Id { get { return Data.Id; } }

		private readonly CharacterData _data;
		public CharacterData Data { get { return _data; } }

		public Hp Hp { get; private set; }
		public readonly Hp HpMax;

		private readonly Passive _passive;
		private readonly SkillManager _skillManager;

		public Character(CharacterData data)
		{
			_data = data;
			Hp = HpMax = data.Stats.Hp.ToValue();
			_passive = PassiveFactory.Create(data.Passive);
			_skillManager = new SkillManager(data.SkillSet);
		}

		public void Hit(Damage dmg)
		{
			Hp -= dmg.Value;
		}

		public void PerformSkill(SkillSlot idx)
		{
			_skillManager[idx].Perform();
		}
	}
}
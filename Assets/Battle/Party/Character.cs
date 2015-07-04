namespace SPRPG.Battle
{
	public class Character
	{
		public Hp Hp { get; private set; }
		public readonly Hp HpMax;

		private readonly Passive _passive;
		private readonly SkillManager _skillManager;

		public Character(CharacterData data)
		{
			Hp = HpMax = data.Stats.Hp.ToValue();
			_passive = PassiveFactory.Create(data.Passive);
			_skillManager = new SkillManager(data.SkillSet);
		}

		public void PerformSkill(SkillSlot idx)
		{
			_skillManager[idx].Perform();
		}
	}
}
namespace SPRPG.Battle
{
	public abstract class SkillActor
	{
		public SkillKey Key { get { return _data.Key; } }
		private readonly SkillBalanceData _data;

		public SkillActor(SkillBalanceData data)
		{
			_data = data;
		}

		public abstract void Perform();
	}

	public class NullSkillActor : SkillActor
	{
		public NullSkillActor() 
			: base(new SkillBalanceData())
		{}

		public override void Perform()
		{}
	}
}
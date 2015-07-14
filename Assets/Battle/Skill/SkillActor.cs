namespace SPRPG.Battle
{
	public abstract class SkillActor
	{
		public SkillKey Key { get { return _data.Key; } }
		public SkillBalanceData Data { get { return _data; } }
		private readonly SkillBalanceData _data;

		protected SkillActor(SkillBalanceData data)
		{
			_data = data;
		}

		public abstract void Perform();
		public abstract void Cancel();
	}
}
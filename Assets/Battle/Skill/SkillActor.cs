using UnityEngine;

namespace SPRPG.Battle
{
	public abstract class SkillActor
	{
		public static Battle Context { get; private set; }
		public static void Reset(Battle context)
		{
			Debug.Assert(Context == null || context == null);
			Context = context;
		}

		public SkillKey Key { get { return _data.Key; } }
		public SkillBalanceData Data { get { return _data; } }
		private readonly SkillBalanceData _data;

		protected SkillActor(SkillBalanceData data)
		{
			_data = data;
		}

		public abstract void Perform();
	}
}
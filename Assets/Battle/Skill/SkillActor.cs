using System;
using Gem;

namespace SPRPG.Battle
{
	public abstract class SkillActor : SkillActorBase
	{
		public SkillKey Key { get { return _data.Key; } }
		public SkillBalanceData Data { get { return _data; } }
		private readonly SkillBalanceData _data;

		public new Action<SkillActor> OnStop;

		protected SkillActor(SkillBalanceData data)
		{
			_data = data;
			base.OnStop += skillActor => OnStop.CheckAndCall((SkillActor)skillActor);
		}
	}
}
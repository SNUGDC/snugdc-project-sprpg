using System;
using Gem;

namespace SPRPG.Battle
{
	public abstract class SkillActor : SkillActorBase
	{
		public SkillKey Key { get { return Data.Key; } }
		public readonly SkillBalanceData Data;
		public readonly Character Owner;

		public new Action<SkillActor> OnStop;

		protected SkillActor(SkillBalanceData data)
		{
			Data = data;
			Owner = owner;
			base.OnStop += skillActor => OnStop.CheckAndCall((SkillActor)skillActor);
		}
	}
}
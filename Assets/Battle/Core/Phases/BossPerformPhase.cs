namespace SPRPG.Battle
{
	public class BossPerformPhase : BattlePhase
	{
		public BossPerformPhase(Battle context) : base(context, BattleState.BossPerform)
		{
		}

		public override void Enter()
		{
			if (!Context.BossAi.IsPerforming)
				Context.BossAi.Perform(Context);
			base.Enter();
		}
	}
}

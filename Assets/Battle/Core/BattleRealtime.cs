namespace SPRPG.Battle
{
	public class BattleRealtime
	{
		private readonly Battle _battle;
		private readonly float _secondPerTick;
		private float _elapsed;

		public BattleRealtime(Battle battle)
		{
			_battle = battle;
			_secondPerTick = 1f / Config.Data.Battle.TickPerSecond;
		}

		public void Update(float dt)
		{
			_elapsed += dt;
			while (_elapsed >= _secondPerTick)
			{
				_elapsed -= _secondPerTick;
				_battle.Scheduler.Proceed();
			}
		}
	}
}

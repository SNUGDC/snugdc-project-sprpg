namespace SPRPG.Battle
{
	public class BattleRealtime
	{
		private readonly Clock _clock;
		private readonly float _secondPerTick;
		private float _elapsed;

		public BattleRealtime(Clock clock)
		{
			_clock = clock;
			_secondPerTick = 1f / Config.Data.Battle.TickPerSecond;
		}

		public void Update(float dt)
		{
			_elapsed += dt;
			while (_elapsed >= _secondPerTick)
			{
				_elapsed -= _secondPerTick;
				_clock.Proceed();
			}
		}
	}
}

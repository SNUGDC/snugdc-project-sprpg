namespace SPRPG.Battle
{
	public class Cooltimer
	{
		public readonly Tick Cooltime;
		public Tick TimeLeft { get; private set; }

		public Cooltimer(Tick cooltime)
		{
			Cooltime = TimeLeft = cooltime;
		}

		public Cooltimer(Tick cooltime, Tick timeLeft)
		{
			Cooltime = cooltime;
			TimeLeft = timeLeft;
		}

		public bool Tick(bool reset = true)
		{
			var ret = --TimeLeft <= 0;
			if (ret && reset) Reset();
			return ret;
		}

		public void Reset()
		{
			TimeLeft = Cooltime;
		}
	}
}

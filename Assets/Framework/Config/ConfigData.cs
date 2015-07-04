namespace SPRPG
{
	public class ConfigData
	{
		public struct Scene_
		{
			public struct Camp_
			{
				public double DecelerationRate;
			}

			public Camp_ Camp;
		}

		public struct Battle_
		{
			public int InputValidBefore;
			public int InputValidAfter;
		}

		public Scene_ Scene;
		public Battle_ Battle;

		public void Build()
		{}
	}
}

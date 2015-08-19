namespace SPRPG
{
	public enum CharacterId
	{
		Warrior = 1,
		Wizard = 2,
		Archer = 3,
		Paladin = 4,
		Thief = 6,
	}

	public static class CharacterConst
	{
		public const int Count = 3;
	}

	public static class CharacterHelper
	{
		public static CharacterId MakeIdWithIndex(int i)
		{
			return (CharacterId)(i + 1);
		}

		public static int ToIndex(this CharacterId thiz)
		{
			return (int) thiz - 1;
		}
	}
}
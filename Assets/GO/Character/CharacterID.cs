namespace SPRPG
{
	public enum CharacterId
	{
		Warrior = 1,
		Wizard = 2,
		Archer = 3,
	}

	public static class CharacterConst
	{
		public const int Count = 3;
	}

	public static class CharacterHelper
	{
		public static CharacterId MakeId(int id)
		{
			return (CharacterId)id;
		}
	}
}
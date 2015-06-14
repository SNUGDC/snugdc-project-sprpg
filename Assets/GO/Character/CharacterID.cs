namespace SPRPG
{
	public enum CharacterID
	{}

	public static class CharacterConst
	{
		public const int Count = 3;
	}

	public static class CharacterHelper
	{
		public static CharacterID MakeID(int id)
		{
			return (CharacterID)id;
		}
	}
}
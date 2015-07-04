namespace SPRPG.Battle
{
	public enum InputType
	{
		Shift,
		Skill,
	}

	public enum InputGrade
	{
		Good, Bad,
	}

	public enum Hp { }

	public static class ExtensionMethods
	{
		public static Hp ToValue(this StatHp thiz)
		{
			return (Hp)(int) thiz;
		}
	}
}

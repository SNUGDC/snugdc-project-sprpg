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

	public enum Element
	{
		Normal,
	}

	public enum Hp { }

	public struct Damage
	{
		public Hp Value;
		public Element Element;
	}

	public static class ExtensionMethods
	{
		public static Hp ToValue(this StatHp thiz)
		{
			return (Hp)(int) thiz;
		}
	}
}

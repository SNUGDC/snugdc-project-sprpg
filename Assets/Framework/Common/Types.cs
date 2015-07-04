namespace SPRPG
{
	public enum PassiveKey { }

	public enum SkillKey { }
	public enum SkillSlot { _1 = 0, _2 = 1, _3 = 2, _4 = 3 }

	public static partial class ExternsionMethods
	{
		public static int ToIndex(this SkillSlot thiz)
		{
			return (int) thiz;
		}
	}
}

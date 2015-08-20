namespace SPRPG.Battle
{
	public struct MonkEquilityArguments
	{
		public Damage Damage;
		public Hp Heal;

		public string Describe(string format)
		{
			format = SkillDescriptorHelper.Replace(format, "Damage", Damage.Value);
			format = SkillDescriptorHelper.Replace(format, "Heal", Heal);
			return format;
		}
	}
}
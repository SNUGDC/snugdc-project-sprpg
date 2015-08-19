namespace SPRPG.Battle
{
	public struct PaladinBashArguments
	{
		public Damage Damage;
		public StunTest StunTest;

		public string Describe(string format)
		{
			format = SkillDescriptorHelper.Replace(format, "Damage", Damage.Value);
			format = SkillDescriptorHelper.Replace(format, "StunPercentage", StunTest.Percentage);
			return format;
		}
	}

}

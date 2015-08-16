using Gem;

namespace SPRPG.Battle
{
	public class WizardIceBoltSkillActor : SingleDelayedPerformSkillActor
	{
		private readonly AttackAndGrantStatusConditionArguments _arguments;

		public WizardIceBoltSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
			_arguments = data.Arguments.ToObject<AttackAndGrantStatusConditionArguments>();
		}

		protected override void Perform()
		{
			Owner.Attack(Context.Boss, _arguments.Damage);
			Context.Boss.TestAndGrant(_arguments.StatusConditionTest);
		}
	}

	public struct WizardLighteningBoltArguments
	{
		public Damage Damage;
		public Damage DamageMember;
		public StunTest StunTest;

		public string Describe(string format)
		{
			format = SkillDescriptorHelper.Replace(format, "Damage", Damage.Value);
			format = SkillDescriptorHelper.Replace(format, "DamageMember", DamageMember.Value);
			format = SkillDescriptorHelper.Replace(format, "StunPercentage", StunTest.Percentage);
			return format;
		}
	}

	public class WizardLighteningBoltSkillActor : SingleDelayedPerformSkillActor
	{
		private readonly WizardLighteningBoltArguments _argument;

		public WizardLighteningBoltSkillActor(SkillBalanceData data, Battle context, Character owner)
			: base(data, context, owner)
		{
			_argument = data.Arguments.ToObject<WizardLighteningBoltArguments>();
		}

		protected override void Perform()
		{
			Owner.Attack(Context.Boss, _argument.Damage);
			Context.Boss.TestAndGrant(_argument.StunTest);
			var aliveMember = Context.Party.TryGetRandomAliveMember();
			if (aliveMember != null) Owner.Attack(aliveMember, _argument.DamageMember);
		}
	}
}

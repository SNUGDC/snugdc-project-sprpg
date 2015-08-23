using Gem;

namespace SPRPG.Battle
{
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
		private Character _friendlyFireTarget;
		private readonly WizardLighteningBoltArguments _argument;

		public WizardLighteningBoltSkillActor(SkillBalanceData data, Battle context, Character owner)
			: base(data, context, owner)
		{
			_argument = data.Arguments.ToObject<WizardLighteningBoltArguments>();
			
		}

		protected override void DoStart()
		{
			base.DoStart();
			var aliveMember = Context.Party.TryGetRandomAliveMember();
			if (aliveMember.HasValue) _friendlyFireTarget = aliveMember.Value.Character;
			else _friendlyFireTarget = null;
		}

		protected override void Perform()
		{
			Owner.Attack(Context.Boss, _argument.Damage);
			Context.Boss.TestAndGrant(_argument.StunTest);
			if (_friendlyFireTarget != null && _friendlyFireTarget.IsAlive) Owner.Attack(_friendlyFireTarget, _argument.DamageMember);
		}

		public override object MakeViewArgument()
		{
			return _friendlyFireTarget;
		}
	}
}

using Gem;
using LitJson;
using UnityEngine;

namespace SPRPG
{
	public delegate string SkillDescriptor(SkillBalanceData data);

	public static class SkillDescriptorFactory
	{
		private static string Replace(this string thiz, string key, object value)
		{
			return SkillDescriptorHelper.Replace(thiz, key, value);
		}

		private static string Replace(this string thiz, JsonData arguments)
		{
			return SkillDescriptorHelper.Replace(thiz, arguments);
		}

		public static SkillDescriptor Create(SkillKey key)
		{
			switch (key)
			{
				case SkillKey.None1:
				case SkillKey.None2:
				case SkillKey.None3:
				case SkillKey.None4:
					return delegate { return "invalid skill: " + key; };

				case SkillKey.PaladinDefense:
				case SkillKey.PaladinPurification:
				case SkillKey.PaladinMassDefense:
					return data => data.DescriptionFormat;
					
				case SkillKey.WarriorAttack:
				case SkillKey.WarriorStrongAttack:
				case SkillKey.WizardFireBolt:
				case SkillKey.WizardFireBall:
				case SkillKey.ArcherAttack:
				case SkillKey.ArcherStrongShot:
				case SkillKey.ThiefAssassination:
					return data =>
					{
						var args = new AttackSkillArguments(data.Arguments);
						return args.Describe(data.DescriptionFormat);
					};

				case SkillKey.WizardIceBolt:
				case SkillKey.WizardIceSpear:
				case SkillKey.ThiefSandAttack:
				case SkillKey.ThiefPoisonAttack:
					return data =>
					{
						var args = data.Arguments.ToObject<AttackAndGrantStatusConditionArguments>();
						return args.Describe(data.DescriptionFormat);
					};

				case SkillKey.PaladinBash:
				case SkillKey.ThiefStab:
					return data =>
					{
						var args = data.Arguments.ToObject<AttackAndTestStunArguments>();
						return args.Describe(data.DescriptionFormat);
					};
					
				case SkillKey.WizardLighteningBolt:
				case SkillKey.WizardLightening:
					return data =>
					{
						var args = data.Arguments.ToObject<Battle.WizardLighteningBoltArguments>();
						return args.Describe(data.DescriptionFormat);
					};

				case SkillKey.WarriorEvasion:
				case SkillKey.WarriorHeal:
				case SkillKey.ArcherEvasion:
				case SkillKey.ArcherArrowRain:
				case SkillKey.PaladinMassHeal:
				case SkillKey.PaladinBigMassHeal:
				case SkillKey.ThiefEvade:
					return data => data.DescriptionFormat.Text.Replace(data.Arguments);
			}

			Debug.LogError(LogMessages.EnumUndefined(key));
			return delegate { return "undefined descriptor for " + key; };
		}
	}

	public static class SkillDescriptorHelper
	{
		public static string Replace(string format, string key, object value)
		{
			return format.Replace('{' + key + '}', value.ToString());
		}

		public static string Replace(string format, JsonData arguments)
		{
			foreach (var arg in arguments.GetDictEnum())
				format = Replace(format, arg.Key, arg.Value);
			return format;
		}
	}
}

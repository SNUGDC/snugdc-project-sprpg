using Gem;
using UnityEngine;

namespace SPRPG
{
	public delegate string SkillDescriptor(SkillBalanceData data);

	public static class SkillDescriptorFactory
	{
		private static string Replace(this string thiz, string key, object value)
		{
			return thiz.Replace('{' + key + '}', value.ToString());
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

				case SkillKey.WarriorAttack:
				case SkillKey.WarriorStrongAttack:
				case SkillKey.ArcherAttack:
					return data =>
					{
						var args = new AttackSkillArguments(data.Arguments);
						return data.DescriptionFormat.Replace(AttackSkillArguments.DamageKey, args.Damage.Value);
					};

				case SkillKey.WarriorEvasion:
					return data =>
					{
						var args = new FiniteSkillArguments(data.Arguments);
						return data.DescriptionFormat.Replace(FiniteSkillArguments.DurationKey, args.Duration);
					};
				case SkillKey.WarriorHeal:
					return data =>
					{
						var args = new HealSkillArguments(data.Arguments);
						return data.DescriptionFormat.Replace(HealSkillArguments.AmountKey, args.Amount);
					};
			}

			Debug.LogError(LogMessages.EnumUndefined(key));
			return delegate { return "undefined descriptor for " + key; };
		}
	}
}

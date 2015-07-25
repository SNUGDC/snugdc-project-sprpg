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
			return thiz.Replace('{' + key + '}', value.ToString());
		}

		private static string Replace(this string thiz, JsonData arguments)
		{
			foreach (var arg in arguments.GetDictEnum())
				thiz = thiz.Replace(arg.Key, arg.Value);
			return thiz;
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
				case SkillKey.ArcherStrongShot:
					return data =>
					{
						var args = new AttackSkillArguments(data.Arguments);
						return data.DescriptionFormat.Replace(AttackSkillArguments.DamageKey, args.Damage.Value);
					};

				case SkillKey.WarriorEvasion:
				case SkillKey.ArcherEvasion:
				case SkillKey.WarriorHeal:
					return data => data.DescriptionFormat.Replace(data.Arguments);
			}

			Debug.LogError(LogMessages.EnumUndefined(key));
			return delegate { return "undefined descriptor for " + key; };
		}
	}
}

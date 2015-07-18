using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public static class SkillFactory
	{
		public static SkillActor Create(SkillKey key, Character owner)
		{
			var data = SkillBalance._.Find(key);
			if (data == null)
				return new NullSkillActor(owner);

			switch (key)
			{
				case SkillKey.None1:
				case SkillKey.None2:
				case SkillKey.None3:
				case SkillKey.None4:
					return new NullSkillActor(data, owner);
				case SkillKey.WarriorAttack:
					return new AttackSkillActor(data, owner);
				case SkillKey.WarriorHeal:
					return new HealSkillActor(data, owner);
			}

			Debug.LogError(LogMessages.EnumNotHandled(key));
			return new NullSkillActor(data, owner);
		}
	}
}

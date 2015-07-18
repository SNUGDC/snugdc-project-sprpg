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
				case SkillKey.WarriorAttack:
					return new AttackSkillActor(data, owner);
			}

			return new NullSkillActor(data);
		}
	}
}

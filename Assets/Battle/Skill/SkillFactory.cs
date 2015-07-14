using Gem;

namespace SPRPG.Battle
{
	public static class SkillFactory
	{
		public static SkillActor Create(SkillKey key)
		{
			var data = SkillBalance._.Find(key);
			if (data == null)
				return new NullSkillActor();

			switch (key)
			{
				case SkillKey.WarriorAttack:
					return new AttackSkillActor(data);
			}

			return new NullSkillActor(data);
		}
	}
}

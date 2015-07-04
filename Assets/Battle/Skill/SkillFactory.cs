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

			var arguments = data.Arguments;

			switch (key)
			{
				case SkillKey.WarriorAttack:
					var damage = arguments["Damage"].ToObject<Damage>();
					return new AttackSkillActor(data, damage);
			}

			return new NullSkillActor(data);
		}
	}
}

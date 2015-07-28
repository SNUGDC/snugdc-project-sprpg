﻿using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public static class SkillFactory
	{
		public static SkillActor Create(SkillKey key, Battle context, Character owner)
		{
			var data = SkillBalance._.Find(key);
			if (data == null)
				return new NullSkillActor(context, owner);

			switch (key)
			{
				case SkillKey.None1:
				case SkillKey.None2:
				case SkillKey.None3:
				case SkillKey.None4:
					return new NullSkillActor(data, context, owner);
				case SkillKey.WarriorAttack:
				case SkillKey.WarriorStrongAttack:
					return new AttackSkillActor(data, context, owner);
				case SkillKey.WarriorHeal:
					return new HealSkillActor(data, context, owner);
				case SkillKey.WarriorEvasion:
				case SkillKey.ArcherEvasion:
					return new EvasionSkillActor(data, context, owner);
				case SkillKey.ArcherAttack:
				case SkillKey.ArcherStrongShot:
					return new ArcherAttackSkillActor(data, context, owner);
				case SkillKey.ArcherArrowRain:
					return new ArcherArrowRainSkillActor(data, context, owner);
			}

			Debug.LogError(LogMessages.EnumNotHandled(key));
			return new NullSkillActor(data, context, owner);
		}
	}
}

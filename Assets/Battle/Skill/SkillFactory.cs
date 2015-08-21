using Gem;
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
				case SkillKey.WizardFireBolt:
				case SkillKey.WizardFireBall:
				case SkillKey.ThiefAssassination:
					return new AttackSkillActor(data, context, owner);
				case SkillKey.WarriorHeal:
				case SkillKey.MonkHeal:
					return new HealSkillActor(data, context, owner);
				case SkillKey.PaladinMassHeal:
				case SkillKey.PaladinBigMassHeal:
					return new MassHealSkillActor(data, context, owner);
				case SkillKey.PaladinPurification:
					return new MassCureSkillActor(data, context, owner);
				case SkillKey.WarriorEvasion:
				case SkillKey.ThiefEvade:
					return new EvasionSkillActor(data, context, owner);
				case SkillKey.WizardIceBolt:
				case SkillKey.WizardIceSpear:
				case SkillKey.ThiefSandAttack:
				case SkillKey.ThiefPoisonAttack:
					return new AttackAndGrantStatusConditionSkillActor(data, context, owner);
				case SkillKey.PaladinBash:
				case SkillKey.ThiefStab:
					return new AttackAndTestStunSkillActor(data, context, owner);

				case SkillKey.WizardLighteningBolt:
				case SkillKey.WizardLightening:
					return new WizardLighteningBoltSkillActor(data, context, owner);

				case SkillKey.ArcherReload:
					return new ArcherReloadSkillActor(data, context, owner);
				case SkillKey.ArcherEvadeShot:
					return new ArcherEvadeShotSkillActor(data, context, owner);
				case SkillKey.ArcherSnipe:
					return new ArcherAttackSkillActor(data, context, owner);
				case SkillKey.ArcherWeakPointAttack:
					return new ArcherWeakPointAttackActor(data, context, owner);
				case SkillKey.ArcherArrowRain:
					return new ArcherArrowRainSkillActor(data, context, owner);

				case SkillKey.PaladinDefense:
					return new PaladinDefenseSkillActor(data, context, owner);
				case SkillKey.PaladinMassDefense:
					return new PaladinDefenseSkillActor(data, context, owner);

				case SkillKey.MonkSacrifice:
					return new MonkSacrificeSkillActor(data, context, owner);
				case SkillKey.MonkEquility:
					return new MonkEquilitySkillActor(data, context, owner);
				case SkillKey.MonkRevenge:
					return new MonkRevengeSkillActor(data, context, owner);
				case SkillKey.MonkRecovery:
					return new MonkRecoverySkillActor(data, context, owner);
			}

			Debug.LogError(LogMessages.EnumNotHandled(key));
			return new NullSkillActor(data, context, owner);
		}
	}
}

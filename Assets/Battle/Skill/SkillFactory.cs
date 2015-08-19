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
					return new AttackSkillActor(data, context, owner);
				case SkillKey.WarriorHeal:
					return new HealSkillActor(data, context, owner);
				case SkillKey.PaladinMassHeal:
				case SkillKey.PaladinBigMassHeal:
					return new MassHealSkillActor(data, context, owner);
				case SkillKey.PaladinPurification:
					return new MassCureSkillActor(data, context, owner);
				case SkillKey.WarriorEvasion:
				case SkillKey.ArcherEvasion:
					return new EvasionSkillActor(data, context, owner);

				case SkillKey.WizardIceBolt:
				case SkillKey.WizardIceSpear:
					return new WizardIceBoltSkillActor(data, context, owner);
				case SkillKey.WizardLighteningBolt:
				case SkillKey.WizardLightening:
					return new WizardLighteningBoltSkillActor(data, context, owner);

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

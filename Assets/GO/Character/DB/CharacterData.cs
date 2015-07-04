using System;
using UnityEngine;

namespace SPRPG
{
	[Serializable]
	public class SkillSetDef
	{
		public SkillKey _1 = SkillKey.None1;
		public SkillKey _2 = SkillKey.None2;
		public SkillKey _3 = SkillKey.None3;
		public SkillKey _4 = SkillKey.None4;
	}

	public class CharacterData : ScriptableObject
	{
		public CharacterId Id;
		public CharacterSkinData Skin;

		public Stats Stats;
		public PassiveKey Passive;
		public SkillSetDef SkillSet;

		public Sprite CampEntryIcon;
		public AnimatorOverrideController CampAnimatorController;
	}
}
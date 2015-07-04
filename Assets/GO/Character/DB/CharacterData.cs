using System;
using UnityEngine;

namespace SPRPG
{
	[Serializable]
	public struct SkillSetDef
	{
		public SkillKey _1;
		public SkillKey _2;
		public SkillKey _3;
		public SkillKey _4;
	}

	public class CharacterData : ScriptableObject
	{
		[SerializeField]
		private int _id;
		public CharacterID ID
		{
			get { return (CharacterID) _id; }
			set { _id = (int) value; }
		}

		public CharacterSkinData Skin;

		public Stats Stats;
		public PassiveKey Passive;
		public SkillSetDef SkillSet;

		public Sprite CampEntryIcon;
		public AnimatorOverrideController CampAnimatorController;
	}
}
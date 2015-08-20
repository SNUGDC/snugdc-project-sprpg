using System;
using UnityEngine;

namespace SPRPG
{
	public class CharacterData : ScriptableObject
	{
		public CharacterId Id;
		[NonSerialized]
		public CharacterBalanceData Balance;
		public CharacterView CharacterView;
		public AnimatorOverrideController CampAnimatorController;
	}
}
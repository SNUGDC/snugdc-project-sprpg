﻿using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine;
using Vector2 = LitJson.Vector2;

namespace SPRPG
{
	public class CampData
	{
		public class Cargo_
		{
			public class Entry
			{
				public Vector3 Position;
			}

			public Vector2 Position;
			public List<Entry> Entries;
		}

		public class Character_
		{
			public Vector3 Position;
			public Vector2 Scale;
			public bool Flip;
			public IntRect BoundingRect;
		}

		public float DecelerationRate;
		public Cargo_ Cargo;
		public List<Character_> Characters;
		public Dictionary<CharacterId, Character_> CharacterDic;

		public void Build()
		{
			var i = 0;

			foreach (var character in Characters)
			{
				if (character.BoundingRect.IsZero())
				{
					character.BoundingRect = new IntRect(-50, 0, 50, 170);
				}
			}

			CharacterDic = Characters.ToDictionary(character => CharacterHelper.MakeIdWithIndex(i++));
		}
	}

	public sealed class CampBalance : Balance<CampData>
	{
		public static readonly CampBalance _ = new CampBalance();

		private CampBalance() : base("camp")
		{}

		protected override void AfterLoad(bool success)
		{
			base.AfterLoad(success);
			Data.Build();
		}
	}
}
﻿#if UNITY_EDITOR

using System;
using System.Collections;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public static class DebugLogger
	{
		static DebugLogger()
		{
			HudEvents.OnSomeCharacterHpChanged.Action += (idx, character, oldHp) =>
			{
				Debug.Log(String.Format("{1}({0})'s hp changed from {2} to {3}.", idx, character.Id, oldHp, character.Hp));
			};

			HudEvents.OnSomeCharacterDead.Action += (idx, character) =>
			{
				Debug.Log(String.Format("{1}({0}) is dead.", idx, character.Id));
			};
		}

		public static void Init()
		{
			// do nothing.
		}
	}
}

#endif
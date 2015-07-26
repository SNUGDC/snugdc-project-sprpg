#if UNITY_EDITOR

using UnityEngine;

namespace SPRPG.Battle.View
{
	public static class DebugLogger
	{
		static DebugLogger()
		{
			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
				AttachMember(idx);

			Events.OnBossSkillStart += (boss, actor) =>
			{
				Debug.Log("boss used skill " + actor.LocalKey);
			};

			Events.OnWin += () => Debug.Log("result: won");
			Events.OnLose += () => Debug.Log("result: lost");
		}

		public static void Init()
		{
			// do nothing.
		}

		private static void AttachMember(OriginalPartyIdx idx)
		{
			HudEvents.GetOnCharacterHpChanged(idx).Value.Action += (idx2, character, oldHp) =>
			{
				Debug.Log(string.Format("{1}({0})'s hp changed from {2} to {3}.", idx2, character.Id, oldHp, character.Hp));
			};

			HudEvents.GetOnCharacterDead(idx).Value.Action += (idx2, character) =>
			{
				Debug.Log(string.Format("{1}({0}) is dead.", idx2, character.Id));
			};
		}
	}
}

#endif
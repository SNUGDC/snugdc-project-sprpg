#if UNITY_EDITOR

using UnityEngine;

namespace SPRPG.Battle.View
{
	public static class DebugLogger
	{
		static DebugLogger()
		{
			AttachInput();
			
			foreach (var idx in BattleHelper.GetOriginalPartyIdxEnumerable())
				AttachMember(idx);

			Events.Boss.OnSkillStart += (boss, actor) =>
			{
				if (!DebugConfig.Battle.Log.BossSkillStart) return;
				Debug.Log("boss used skill " + actor.LocalKey);
			};

			Events.OnWin += () => Debug.Log("result: won");
			Events.OnLose += () => Debug.Log("result: lost");
		}

		public static void Init()
		{
			// do nothing.
		}

		private static void AttachInput()
		{
			Events.OnInputSkill += termAndGrade =>
			{
				if (!DebugConfig.Battle.Log.Input) return;
				Debug.Log(termAndGrade);
			};

			Events.OnInputShift += termAndGrade =>
			{
				if (!DebugConfig.Battle.Log.Input) return;
				Debug.Log(termAndGrade);
			};
		}

		private static void AttachMember(OriginalPartyIdx idx)
		{
			var characterEvents = Events.GetCharacter(idx);
			characterEvents.OnHpChanged += (idx2, character, oldHp) =>
			{
				if (!DebugConfig.Battle.Log.CharacterHpChanged) return;
				Debug.Log(string.Format("{1}({0})'s hp changed from {2} to {3}.", idx2, character.Id, oldHp, character.Hp));
			};

			characterEvents.OnDead += (idx2, character) =>
			{
				if (!DebugConfig.Battle.Log.CharacterDead) return;
				Debug.Log(string.Format("{1}({0}) is dead.", idx2, character.Id));
			};
		}
	}
}

#endif
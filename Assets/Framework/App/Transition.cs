﻿using System.Collections.Generic;
using Gem;
using SPRPG.Profile;
using UnityEngine;

namespace SPRPG
{
	public enum SceneType
	{
		Camp, 
		World, 
		Battle, 
		Profile, 
		Setting,
	}

	public class TransitionStack
	{
		private const int Capacity = 10;
		private readonly LinkedList<SceneType> _stack = new LinkedList<SceneType>();

		public void Push(SceneType scene)
		{
			_stack.AddLast(scene);
			while (_stack.Count > Capacity)
				_stack.RemoveFirst();
		}

		public void RemoveLast()
		{
			_stack.RemoveLast();
		}

		public SceneType PopOrDefault(SceneType defaultScene)
		{
			return _stack.PopOrDefault(defaultScene);
		}

		public void Clear() { _stack.Clear(); }
	}

	public static class Transition
	{
		private const string CampSceneName = "Camp";
		private const string WorldSceneName = "World";
		private const string BattleSceneName = "Battle";
		private const string ProfileSceneName = "Profile";
		private const string SettingSceneName = "Setting";

		private static readonly TransitionStack _log = new TransitionStack();

		public static void ClearLog() { _log.Clear(); }

		private static void LoadLevel(string scene)
		{
			Time.timeScale = 1;
			Application.LoadLevel(scene);
		}

		public static void TransferToCamp()
		{
			_log.Push(SceneType.Camp);
			LoadLevel(CampSceneName);
		}

		public static void TransferToWorld()
		{
			_log.Push(SceneType.World);
			LoadLevel(WorldSceneName);
		}

		public static void TransferToBattleWithUserBattleDef(StageId stage)
		{
			TransferToBattle(UserUtil.MakeBattleDef(stage));
		}

		public static void TransferToBattleWithoutDef()
		{
			Debug.LogWarning("trying to transfer without def. sure?");
			DoTransferToBattle();
		}

		public static void TransferToBattle(Battle.BattleDef def)
		{
			Battle.BattleWrapper.Def = def;
			DoTransferToBattle();
		}
		
		private static void DoTransferToBattle()
		{
			_log.Push(SceneType.Battle);
			LoadLevel(BattleSceneName);
		}

		public static void TransferToProfile(CharacterId character)
		{
			ProfileController.CharacterToShow = character;
			JustTransferToProfile();
		}
		
		public static void TransferToProfileWithPreviousCharacter()
		{
			JustTransferToProfile();
		}

		private static void JustTransferToProfile()
		{
			_log.Push(SceneType.Profile);
			LoadLevel(ProfileSceneName);
		}

		public static void TransferToSetting()
		{
			_log.Push(SceneType.Setting);
			LoadLevel(SettingSceneName);
		}

		public static void TransferToPreviousScene(SceneType defaultScene)
		{
			// remove current scene.
			_log.RemoveLast();

			// transfer to previous scene.
			var scene = _log.PopOrDefault(defaultScene);
			Transfer(scene);
		}

		private static void Transfer(SceneType scene)
		{
			switch (scene)
			{
				case SceneType.Camp: TransferToCamp(); break;
				case SceneType.World: TransferToWorld(); break;
				case SceneType.Battle: TransferToBattleWithoutDef(); break;
				case SceneType.Profile: TransferToProfileWithPreviousCharacter(); break;
				case SceneType.Setting: TransferToSetting(); break;
				default:
					// do nothing.
					Debug.LogError(LogMessages.EnumNotHandled(scene));
					break;
			}
		}
	}
}
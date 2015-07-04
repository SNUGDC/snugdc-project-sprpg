using System;
using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public enum InputReceiverType
	{
#if KEYBOARD_SUPPORTED
		Keyboard,
#endif
	}

	public abstract class InputReceiver
	{
		public Action OnSkill;
		public Action OnShift;

		public abstract void Update(float dt);

		public void ForceSkill()
		{
			CheckAndInvokeSkill();
		}

		public void ForceShift()
		{
			CheckAndInvokeShift();
		}

		protected void CheckAndInvokeSkill()
		{
			OnSkill.CheckAndCall();
		}

		protected void CheckAndInvokeShift()
		{
			OnShift.CheckAndCall();
		}
	}

#if KEYBOARD_SUPPORTED
	public sealed class KeyboardInputReceiver : InputReceiver
	{
		private const KeyCode SkillKey = KeyCode.X;
		private const KeyCode ShiftKey = KeyCode.Z;

		public override void Update(float dt)
		{
			if (Input.GetKeyDown(SkillKey))
			{
				CheckAndInvokeSkill();
				return;
			}

			if (Input.GetKeyDown(ShiftKey))
			{
				CheckAndInvokeShift();
				return;
			}
		}
	}
#endif

	public static class InputReceiverFactory
	{
		public static InputReceiver Create(InputReceiverType type)
		{
			switch (type)
			{
#if KEYBOARD_SUPPORTED
				case InputReceiverType.Keyboard:
					return new KeyboardInputReceiver();
#endif
			}

			return null;
		}

		public static InputReceiver CreatePlatformPreferred()
		{
#if KEYBOARD_SUPPORTED
			return new KeyboardInputReceiver();
#else
#error undefined platform
#endif
		}
	}
}
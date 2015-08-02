using Gem;
using LitJson;
using UnityEngine;

namespace SPRPG.Battle
{
	public enum ActTarget
	{
		Animator,
	}

	public enum ActAction
	{
		SetTrigger,
	}

	public struct ActData
	{
		public ActTarget Target;
		public ActAction Action;
		public JsonData Argument;
	}

	public class ActContext
	{
		public virtual Animator Animator { get { return null; } }
	}

	public abstract class Act
	{
		public abstract void Do(ActContext context);
	}

	public sealed class ActAnimatorSetTrigger : Act
	{
		private readonly string _trigger;

		public ActAnimatorSetTrigger(string trigger)
		{
			_trigger = trigger;
		}

		public override void Do(ActContext context)
		{
			context.Animator.SetTrigger(_trigger);
		}
	}

	public static class ActFactory
	{
		public static Act Create(ActData data)
		{
			switch (data.Target)
			{
				case ActTarget.Animator: return ActAnimatorFactory.Create(data.Action, data.Argument);
			}

			Debug.LogError(LogMessages.EnumNotHandled(data.Target));
			return null;
		}
	}
	
	public static class ActAnimatorFactory
	{
		public static ActAnimatorSetTrigger CreateSetTrigger(string trigger) { return new ActAnimatorSetTrigger(trigger); }

		public static Act Create(ActAction action, JsonData argument)
		{
			switch (action)
			{
				case ActAction.SetTrigger: return CreateSetTrigger((string)argument);
			}

			Debug.LogError(LogMessages.EnumNotHandled(action));
			return null;
		}
	}
}
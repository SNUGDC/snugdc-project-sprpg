using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public class StatusConditionFsm
	{
		public bool IsActive { get { return DurationLeft > 0; } }
		public Tick Duration { get; private set; }
		public Tick DurationLeft { get { return Duration - (int)TotalElapsed; } }
		public Tick TotalElapsed { get; private set; }

		public StatusConditionFsm(Tick duration)
		{
			Duration = duration;
		}

		public void Tick()
		{
			if (!IsActive)
			{
				Debug.LogError("not active.");
				return;
			}
			++TotalElapsed;
		}
	}

	public abstract class StatusConditionActor
	{
		public readonly StatusConditionType Type;
		protected StatusConditionActor(StatusConditionType type) { Type = type; }
		public abstract void Tick(Pawn pawn, Tick totalElapsed);
	}

	public class StatusCondition
	{
		public StatusConditionType Type { get { return _actor.Type; } }
		public bool IsActive { get { return _fsm.IsActive; } }
		public Tick DurationLeft { get { return _fsm.DurationLeft; } }

		private readonly Pawn _pawn;
		private readonly StatusConditionFsm _fsm;
		private readonly StatusConditionActor _actor;

		public StatusCondition(Pawn pawn, StatusConditionActor actor, Tick duration)
		{
			_pawn = pawn;
			_actor = actor;
			_fsm = new StatusConditionFsm(duration);
		}

		public void Tick()
		{
			if (!_fsm.IsActive)
			{
				Debug.LogError("tick but not active.");
				return;
			}
			
			_fsm.Tick();
			_actor.Tick(_pawn, _fsm.TotalElapsed);
		}
	}

	public sealed class StatusConditionPoisonActor : StatusConditionActor
	{
		public const Tick Cooltime = Const.Term;
		public const Hp Damage = (Hp) 30;

		public StatusConditionPoisonActor() : base(StatusConditionType.Poison) { }

		private static bool IsTriggered(Tick totalElapsed)
		{
			return (int) totalElapsed%(int) Cooltime == 0;
		}

		public override void Tick(Pawn pawn, Tick totalElapsed)
		{
			if (!IsTriggered(totalElapsed)) return;
			pawn.Hit(new Damage(Damage, Element.Poison));
		}
	}

	public static class StatusConditionFactory
	{
		public static StatusCondition Create(Pawn pawn, StatusConditionType type, Tick duration)
		{
			switch (type)
			{
				case StatusConditionType.Poison:
					return new StatusCondition(pawn, new StatusConditionPoisonActor(), duration);
			}

			Debug.LogError(LogMessages.EnumNotHandled(type));
			return null;
		}
	}
}

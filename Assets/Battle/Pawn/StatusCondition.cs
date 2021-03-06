﻿using Gem;
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

		public bool Reserve(Tick duration)
		{
			if (DurationLeft > duration)
				return false;
			Duration = duration;
			return true;
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

	public class StatusConditionActor
	{
		public readonly StatusConditionType Type;
		public StatusConditionActor(StatusConditionType type) { Type = type; }
		public virtual void Tick(Pawn pawn, Tick totalElapsed) { }
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

		public bool Reserve(Tick duration)
		{
			return _fsm.Reserve(duration);
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

		public static implicit operator StatusConditionType(StatusCondition thiz) { return thiz.Type; }
	}

	public sealed class StatusConditionPoisonActor : StatusConditionActor
	{
		private readonly StatusConditionPoisonBalanceData _data;

		public StatusConditionPoisonActor(StatusConditionPoisonBalanceData data) : base(StatusConditionType.Poison)
		{
			_data = data;
		}

		private bool IsTriggered(Tick totalElapsed)
		{
			return (int) totalElapsed%(int)_data.Cooltime == 0;
		}

		public override void Tick(Pawn pawn, Tick totalElapsed)
		{
			if (!IsTriggered(totalElapsed)) return;
			pawn.Hit(new Damage(_data.Damage, Element.Poison));
		}
	}

	public static class StatusConditionFactory
	{
		public static StatusCondition Create(Pawn pawn, StatusConditionType type, Tick duration)
		{
			switch (pawn.StatusConditionGroup)
			{
				case StatusConditionGroup.Character:
					return CreateCharacter(pawn, type, duration);
				case StatusConditionGroup.Boss:
					return CreateBoss(pawn, type, duration);
				default:
					Debug.LogError(LogMessages.EnumNotHandled(type));
					return null;
			}
		}

		public static StatusCondition CreateCharacter(Pawn pawn, StatusConditionType type, Tick duration)
		{
			var statusConditions = BattleBalance._.Data.Character.StatusConditions;
			switch (type)
			{
				case StatusConditionType.Poison:
					var data = statusConditions.GetPoison();
					return new StatusCondition(pawn, new StatusConditionPoisonActor(data), duration);
				default:
					Debug.LogError(LogMessages.EnumNotHandled(type));
					return null;
			}
		}

		public static StatusCondition CreateBoss(Pawn pawn, StatusConditionType type, Tick duration)
		{
			var statusConditions = BattleBalance._.Data.Boss.StatusConditions;
			switch (type)
			{
				case StatusConditionType.Freeze:
				case StatusConditionType.Blind:
					return new StatusCondition(pawn, new StatusConditionActor(type), duration);
				case StatusConditionType.Poison:
					var data = statusConditions.GetPoison();
					return new StatusCondition(pawn, new StatusConditionPoisonActor(data), duration);
				default:
					Debug.LogError(LogMessages.EnumNotHandled(type));
					return null;
			}
		}
	}
}

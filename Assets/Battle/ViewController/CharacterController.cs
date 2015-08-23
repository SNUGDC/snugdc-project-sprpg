using Gem;
using UnityEngine;

namespace SPRPG.Battle.View
{
	public class CharacterController
	{
		private readonly CharacterView _view;
		public CharacterView View { get { return _view; } }
		private readonly Character _character;

		public CharacterController(CharacterView view, Character character)
		{
			_view = view;
			_character = character;
			_character.OnAfterHit += OnAfterHit;
			_character.OnDead += OnDead;
			_character.OnSkillStart += OnSkillStart;
			_character.Guard.OnChanged += OnGuardChanged;
			_character.OnGrantStatusCondition += OnGrantStatusCondition;
			_character.OnStopStatusCondition += OnStopStatusCondition;
		}

		~CharacterController()
		{
			_character.OnAfterHit -= OnAfterHit;
			_character.OnDead -= OnDead;
			_character.OnSkillStart -= OnSkillStart;
			_character.Guard.OnChanged -= OnGuardChanged;
			_character.OnGrantStatusCondition -= OnGrantStatusCondition;
			_character.OnStopStatusCondition -= OnStopStatusCondition;
		}

		private void OnAfterHit(Character character, Damage damage)
		{
			View.Hit();
		}

		private void OnDead(Character character)
		{
			_view.Dead();
		}

		private void OnSkillStart(Character character, SkillSlot skillSlot, SkillActor skillActor)
		{
			_view.PlaySkillStart(skillActor.Data, skillActor.MakeViewArgument());
		}

		private void OnGuardChanged(bool enabled)
		{
			if (enabled)
				_view.PlayGuardAura();
			else
				_view.StopGuardAura();
		}

		private void OnGrantStatusCondition(Character character, StatusConditionType type)
		{
			if (type == StatusConditionType.Poison)
				View.PlayStatusConditionPoison();
			else
				Debug.LogError(LogMessages.EnumNotHandled(type));
		}

		private void OnStopStatusCondition(Character character, StatusConditionType type)
		{
			if (type == StatusConditionType.Poison)
				View.StopStatusConditionPoison();
			else
				Debug.LogError(LogMessages.EnumNotHandled(type));
		}
	}
}

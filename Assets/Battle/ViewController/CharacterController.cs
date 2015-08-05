﻿namespace SPRPG.Battle.View
{
	public class CharacterController
	{
		private readonly CharacterView _view;
		private readonly Character _character;

		public CharacterController(CharacterView view, Character character)
		{
			_view = view;
			_character = character;
			_character.OnDead += OnDead;
			_character.OnSkillStart += OnSkillStart;
		}

		~CharacterController()
		{
			_character.OnDead -= OnDead;
			_character.OnSkillStart -= OnSkillStart;
		}

		private void OnDead(Character character)
		{
			_view.Dead();
		}

		private void OnSkillStart(Character character, SkillSlot skillSlot, SkillActor skillActor)
		{
			_view.PlaySkillStart(skillActor.Data);
		}
	}
}
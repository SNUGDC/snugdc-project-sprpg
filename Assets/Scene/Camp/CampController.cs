using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG
{
	public class CampController 
		: MonoBehaviour
	{
		[SerializeField]
		private Transform _foregroundRoot;
		[SerializeField]
		private Transform _worldRoot;

		[SerializeField]
		private ScrollRect _scrollRect;
		[SerializeField]
		private Transform _worldAnchor;

		private readonly Dictionary<CharacterID, CampCharacter> _characters = new Dictionary<CharacterID, CampCharacter>();

		void Start()
		{
			CampCharacter.ForegroundRoot = _foregroundRoot;

			var config = Config.Data.Scene.Camp;
			_scrollRect.decelerationRate = (float)config.DecelerationRate;

			foreach (var character in UserCharacters.GetEnumerable())
				AddCharacter(character);
		}

		void LateUpdate()
		{
			_foregroundRoot.transform.position = _worldAnchor.position;
			_worldRoot.transform.position = _worldAnchor.position;
		}

		void AddCharacter(UserCharacter userCharacter)
		{
			var character = CampCharacter.Instantiate(userCharacter);
			character.transform.SetParent(_worldRoot, false);
			_characters[userCharacter.ID] = character;
		}
	}
}
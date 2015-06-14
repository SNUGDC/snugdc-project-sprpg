using System.Collections.Generic;
using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG
{
	public class CampController 
		: MonoBehaviour
	{
		[SerializeField]
		private Transform _worldRoot;

		[SerializeField]
		private ScrollRect _scrollRect;
		[SerializeField]
		private Transform _worldAnchor;

		private readonly Dictionary<CharacterID, CampCharacter> _characters = new Dictionary<CharacterID, CampCharacter>();

		void Start()
		{
			var config = Config.Data.Scene.Camp;
			_scrollRect.decelerationRate = (float)config.DecelerationRate;

			foreach (var character in UserCharacters.GetEnumerable())
				AddCharacter(character);
		}

		void Update()
		{
			_worldRoot.transform.position = _worldAnchor.position;
		}

		void AddCharacter(UserCharacter userCharacter)
		{
			var character = CampCharacter.Instantiate(userCharacter);
			var id = userCharacter.ID;

			CampData.Character_ characterData;
			if (CampBalance._.Data.CharacterDic.TryGet(id, out characterData))
			{
				character.transform.SetParent(_worldRoot, false);
				character.transform.localPosition = characterData.Position;
			}

			_characters[id] = character;
		}
	}
}
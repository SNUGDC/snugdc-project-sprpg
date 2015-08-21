using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Camp
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

		[SerializeField]
		private CargoController _cargo;

		private readonly Dictionary<CharacterId, CampCharacter> _characters = new Dictionary<CharacterId, CampCharacter>();

		void Start()
		{
			CampCharacter.ForegroundRoot = _foregroundRoot;

			var campBalance = CampBalance._.Data;
			_scrollRect.decelerationRate = campBalance.DecelerationRate;
			_cargo.transform.localPosition = campBalance.Cargo.Position;

			foreach (var character in UserCharacters.GetEnumerable())
				AddCharacter(character);
		}

		void LateUpdate()
		{
			var position = _worldAnchor.position;
			position.x *= 1.3f;
			_foregroundRoot.transform.position = position;
			_worldRoot.transform.position = _worldAnchor.position;
		}

		void AddCharacter(UserCharacter userCharacter)
		{
			var character = CampCharacter.Instantiate(userCharacter);
			character.transform.SetParent(_worldRoot, false);
			_characters[userCharacter.Id] = character;
		}

		public void GotoWorld()
		{
			Transition.TransferToWorld();
		}
	}
}
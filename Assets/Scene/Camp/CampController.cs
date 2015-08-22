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
		private Animator _animator;

		[SerializeField]
		private CargoController _cargo;
		[SerializeField]
		private GameObject _skyray;

		private bool _goingToWorld;

		private readonly Dictionary<CharacterId, CampCharacter> _characters = new Dictionary<CharacterId, CampCharacter>();

		void Start()
		{
			CampCharacter.ForegroundRoot = _foregroundRoot;

			var campBalance = CampBalance._.Data;
			_scrollRect.decelerationRate = campBalance.DecelerationRate;
			_cargo.transform.localPosition = campBalance.Cargo.Position;

			foreach (var character in UserCharacters.GetEnumerable())
				AddCharacter(character);
			Invoke("SyncCharacterPositions", 0);
		}

		void LateUpdate()
		{
			var position = _worldAnchor.position;
			_foregroundRoot.transform.position = position;
			_worldRoot.transform.position = position;
		}

		void AddCharacter(UserCharacter userCharacter)
		{
			var character = CampCharacter.Instantiate(userCharacter);
			character.transform.SetParent(_worldRoot, false);
			character.OnSelectCallback += OnSelectCharacter;
			_characters[userCharacter.Id] = character;
		}

		void SyncCharacterPositions()
		{
			foreach (var kv in _characters)
				kv.Value.SyncPosition();
		}

		public void GotoWorld()
		{
			if (_goingToWorld) return;
			_goingToWorld = true;
			_animator.SetTrigger("GotoWorld");
			Invoke("GotoWorldWithoutAnimation", 1);
		}

		private void GotoWorldWithoutAnimation()
		{
			Transition.TransferToWorld();
		}

		private void SetAllCharacterColor(Color color)
		{
			foreach (var kv in _characters)
				kv.Value.CharacterView.Skeleton.SetColorRecursive(color);
		}

		private void OnSelectCharacter(CampCharacter character, bool isSelected)
		{
			_skyray.SetActive(isSelected);
			if (isSelected)
			{
				_animator.SetTrigger("SelectCharacter");
				_skyray.transform.position = character.transform.position;
				SetAllCharacterColor(Color.grey);
				character.CharacterView.Skeleton.SetColorRecursive(Color.white);
			}
			else
			{
				_animator.SetTrigger("DeselectCharacter");
				SetAllCharacterColor(Color.white);
			}
		}
	}
}
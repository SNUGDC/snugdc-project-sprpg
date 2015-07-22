using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Camp
{
	public class CharacterTool : MonoBehaviour
	{
		private enum EntryState { NotIn, In, }

		private const string EntryInText = Sentence.PartyEntryIn;
		private const string EntryOutText = Sentence.PartyEntryOut;
		private const float CloseDelay = 0.1f;

		public static CharacterTool Current { get; private set; }

		public CharacterId Id { get; private set; }

		private Party _party { get { return Party._; } }

		private EntryState _entryState = EntryState.NotIn;

		[SerializeField]
		private Button _entryButton;
		[SerializeField]
		private Text _entryText;

		public static CharacterTool Open(CharacterId id)
		{
			CloseCurrent();
			Current = Assets._.CampCharacterTool.Instantiate();
			Current.Id = id;
			return Current;
		}

		public static void CloseCurrent()
		{
			if (Current == null) return;
			Current.Close();
			Current = null;
		}

		void Start()
		{
			Debug.Assert(Current == this);
			Refresh();
		}

		void Update()
		{
			if (Input.GetMouseButtonUp(0))
				Invoke("Close", CloseDelay);
		}

		public void Close()
		{
			Debug.Assert(Current == this);
			if (Current == this)
				Current = null;
			Destroy(gameObject);
		}

		private void Refresh()
		{
			var _entity = _party.Find(Id);
			if (_entity == null)
			{
				_entryState = EntryState.NotIn;
				_entryText.text = EntryInText;
				_entryButton.interactable = !_party.IsFull;
			}
			else
			{
				_entryState = EntryState.In;
				_entryText.text = EntryOutText;
				_entryButton.interactable = true;
			}
		}

		public void OnSelectEntry()
		{
			if (_entryState == EntryState.In)
			{
				if (!_party.Remove(Id))
					Debug.Assert(false, "entry remove failed: " + Id);
			}
			else if (_entryState == EntryState.NotIn)
			{
				var character = UserCharacters.Find(Id);
				Debug.Assert(character != null, "Character is not owned by user: " + Id);
				if (character != null)
				{
					if (!_party.TryAdd(character))
						Debug.Assert(false, "Character add failed: " + Id);
				}
			}

			Close();
		}

		public void OnSelectCharacterWindow()
		{
		}
	}
}
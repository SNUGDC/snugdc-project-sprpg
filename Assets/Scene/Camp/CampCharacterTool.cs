using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG
{
	public class CampCharacterTool : MonoBehaviour
	{
		private enum EntryState { NotIn, In, }

		private const string EntryInText = Sentence.PartyEntryIn;
		private const string EntryOutText = Sentence.PartyEntryOut;

		public static CampCharacterTool Current { get; private set; }

		public CharacterID ID { get; private set; }

		private Party _party { get { return Party._; } }

		private EntryState _entryState = EntryState.NotIn;

		[SerializeField]
		private Button _entryButton;
		[SerializeField]
		private Text _entryText;

		public static CampCharacterTool Open(CharacterID id)
		{
			CloseCurrent();
			Current = Assets._.CampCharacterTool.Instantiate();
			Current.ID = id;
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

		public void Close()
		{
			Debug.Assert(Current == this);
			if (Current == this)
				Current = null;
			Destroy(gameObject);
		}

		private void Refresh()
		{
			var _entity = _party.Find(ID);
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
				if (!_party.Remove(ID))
					Debug.Assert(false, "entry remove failed: " + ID);
			}
			else if (_entryState == EntryState.NotIn)
			{
				var character = UserCharacters.Find(ID);
				Debug.Assert(character != null, "Character is not owned by user: " + ID);
				if (character != null)
				{
					if (!_party.TryAdd(character))
						Debug.Assert(false, "Character add failed: " + ID);
				}
			}

			Close();
		}

		public void OnSelectCharacterWindow()
		{
			
		}

		public void OnSelectCancel()
		{
			Close();
		}
	}
}
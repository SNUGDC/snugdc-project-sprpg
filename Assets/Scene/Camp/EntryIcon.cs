using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Camp
{
	public partial class EntryIcon : MonoBehaviour
	{
		[SerializeField]
		private PartyIdx _idx;
		public PartyIdx Idx { get { return _idx; } }
		public CharacterID? Character { get; private set; }

		private Party _party { get { return Party._; } }

		[SerializeField]
		private Image _characterIcon;

		[SerializeField]
		private Button _standByButton;

		void Start()
		{
			_standByButton.interactable = false;

			RegisterPartyCallback(Idx);

			Refresh();
		}

		void OnDestroy()
		{
			UnregisterPartyCallback(Idx);
		}

		void Update()
		{
			UpdateDrag();
		}

		void RegisterPartyCallback(PartyIdx idx)
		{
			_party.GetOnAdd(idx).Value += OnPartyAdded;
			_party.GetOnRemove(idx).Value += OnPartyRemoved;
		}

		void UnregisterPartyCallback(PartyIdx idx)
		{
			_party.GetOnAdd(idx).Value -= OnPartyAdded;
			_party.GetOnRemove(idx).Value -= OnPartyRemoved;
		}

		public void JustSetIdx(PartyIdx idx)
		{
			UnregisterPartyCallback(_idx);
			_idx = idx;
			RegisterPartyCallback(_idx);
		}

		public void SetCharacter(CharacterID id)
		{
			var data = CharacterDB._.Find(id);
			if (data == null) return;

			Character = id;
			_characterIcon.sprite = data.CampEntryIcon;
			_characterIcon.SetNativeSize();
			_standByButton.interactable = true;
		}

		public void RemoveCharacter()
		{
			if (Character == null)
				return;

			Character = null;
			_characterIcon.sprite = Assets._.CampEntryEmptySprite;
			_characterIcon.SetNativeSize();
			_standByButton.interactable = false;
			_party.Remove(Idx);
		}
		
		private void Refresh()
		{
			var entry = _party.Get(Idx);
			if (entry != null)
			{
				SetCharacter(entry.Character);
			}
			else
			{
				RemoveCharacter();
			}
		}

		public void OnSelectStandBy()
		{
			Debug.Assert(Character.HasValue);
			RemoveCharacter();
		}

		private void OnPartyAdded(PartyEntry entry)
		{
			SetCharacter(entry.Character);
		}

		private void OnPartyRemoved(CharacterID character)
		{
			RemoveCharacter();
		}
	}
}
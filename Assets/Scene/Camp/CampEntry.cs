using UnityEngine;
using UnityEngine.UI;

namespace SPRPG
{
	public partial class CampEntry : MonoBehaviour
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

			_party.GetOnAdd(Idx).Value += OnPartyAdded;
			_party.GetOnRemove(Idx).Value += OnPartyRemoved;

			Refresh();
		}

		void OnDestroy()
		{
			_party.GetOnAdd(Idx).Value -= OnPartyAdded;
			_party.GetOnRemove(Idx).Value -= OnPartyRemoved;
		}

		void Update()
		{
			UpdateDrag();
		}

		public void JustSetIdx(PartyIdx idx)
		{
			_idx = idx;
		}

		public void SetCharacter(CharacterID id)
		{
			var data = CharacterDB.Find(id);
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
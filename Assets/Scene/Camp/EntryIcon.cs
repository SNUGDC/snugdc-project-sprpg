using Gem;
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG.Camp
{
	public class EntryIcon : MonoBehaviour
	{
		[SerializeField]
		private PartyIdx _idx;
		public PartyIdx Idx { get { return _idx; } }
		public CharacterId? Character { get; private set; }

		public EntryIconDrag Drag;

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

			Drag.OnDragStay += (entryIconDrag, pointerEventData) =>
			{
				var parentRect = transform.parent.GetRectTransform();
				var parentSize = parentRect.offsetMax - parentRect.offsetMin;
				var anchor = pointerEventData.position.ScaleInverse(parentSize);

				var rect = this.GetRectTransform();
				rect.anchorMin = anchor;
				rect.anchorMax = anchor;
				rect.anchoredPosition = Vector2.zero;
			};
		}

		void OnDestroy()
		{
			UnregisterPartyCallback(Idx);
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

		public void SetCharacter(CharacterId id)
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

		private void OnPartyRemoved(CharacterId character)
		{
			RemoveCharacter();
		}
	}
}
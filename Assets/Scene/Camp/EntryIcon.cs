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

		void Start()
		{
			RegisterPartyCallback(Idx);

			Refresh();

			Drag.OnDragStay += (entryIconDrag, pointerEventData) =>
			{
				var worldPointer = pointerEventData.pressEventCamera.ScreenToWorldPoint(pointerEventData.position);
				var localPointer = (Vector2)transform.parent.worldToLocalMatrix.MultiplyPoint(worldPointer);
				transform.localPosition = localPointer;
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
			var data = CharacterDb._.Find(id);
			if (data == null) return;

			Character = id;
			_characterIcon.sprite = R.Character.LoadIcon(id);
			_characterIcon.SetNativeSize();
		}

		public void RemoveCharacter()
		{
			if (Character == null)
				return;

			Character = null;
			_characterIcon.sprite = Assets._.CampEntryEmptySprite;
			_characterIcon.SetNativeSize();
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

		private void OnPartyAdded(PartyMember entry)
		{
			SetCharacter(entry.Character);
		}

		private void OnPartyRemoved(CharacterId character)
		{
			RemoveCharacter();
		}
	}
}
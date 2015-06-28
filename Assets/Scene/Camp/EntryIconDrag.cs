using System;
using Gem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SPRPG.Camp
{
	public class EntryIconDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public bool IsDragging { get; private set; }

		public Action<EntryIconDrag> OnDragBegin;
		public Action<EntryIconDrag> OnDragEnd;
		public Action<EntryIconDrag, PointerEventData> OnDragStay;

		public void OnPointerDown(PointerEventData eventData)
		{
			IsDragging = true;
			OnDragBegin.CheckAndCall(this);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			IsDragging = false;
			OnDragEnd.CheckAndCall(this);
		}

		public void OnDrag(PointerEventData eventData)
		{
			OnDragStay.CheckAndCall(this, eventData);
		}

	}
}

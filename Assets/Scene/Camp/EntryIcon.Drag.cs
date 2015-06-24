using System;
using Gem;
using UnityEngine;

namespace SPRPG.Camp
{
	public partial class EntryIcon
	{
		public bool IsDragging { get; private set; }

		public Action<EntryIcon> OnDragBegin;
		public Action<EntryIcon> OnDragEnd;

		public void UpdateDrag()
		{
			if (!IsDragging)
				return;

			if (Input.GetMouseButtonUp(0))
			{
				IsDragging = false;
				OnDragEnd.CheckAndCall(this);
				return;
			}

			this.GetRectTransform().TranslateAnchor(Mouse.DeltaPosition);
		}

		public void OnPressDownIcon()
		{
			IsDragging = true;
			OnDragBegin.CheckAndCall(this);
		}
	}
}

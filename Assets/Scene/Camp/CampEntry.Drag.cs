using System;
using Gem;
using UnityEngine;

namespace SPRPG.Camp
{
	public partial class CampEntry
	{
		public bool IsDragging { get; private set; }

		public Action<CampEntry> OnDragBegin;
		public Action<CampEntry> OnDragEnd;

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

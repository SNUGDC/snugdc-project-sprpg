using System;
using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG.Camp
{
	public class EntryIconAligner : MonoBehaviour
	{
		private const float AnchorXStart = 0.58f;
		private const float AnchorXInterval = 0.16f;
		private const float AnchorY = 0.85f;

		private const float AlignLerpScale = 10;
		private const float AlignRangeY = 0.2f;
		private const float AlignThresholdX = 0.1f;

		[SerializeField]
		private List<EntryIcon> _entries;

		private EntryIcon _dragging;

		private Func<PartyIdx, PartyIdx> _idxMapper = idx => idx;

		private static Vector2 GetAnchor(PartyIdx idx)
		{
			switch (idx)
			{
				case PartyIdx._1:
					return new Vector2(AnchorXStart, AnchorY);
				case PartyIdx._2:
					return new Vector2(AnchorXStart + AnchorXInterval, AnchorY);
				case PartyIdx._3:
					return new Vector2(AnchorXStart + AnchorXInterval*2, AnchorY);
				default:
					Debug.Assert(false, LogMessages.EnumUndefined(idx));
					return Vector2.zero;
			}
		}

		void Start()
		{
			foreach (var entry in _entries)
			{
				var entryLocal = entry;
				entry.Drag.OnDragBegin += delegate { OnDragBegin(entryLocal); };
				entry.Drag.OnDragEnd += delegate { OnDragEnd(entryLocal); };
			}
		}

		void Update()
		{
			Align(Time.deltaTime * AlignLerpScale);
		}

		private void Align(float lerp)
		{
			if (_dragging)
			{
				var y = _dragging.GetRectTransform().anchorMin.y;
				if (Mathf.Abs(y - AnchorY) < AlignRangeY)
				{
					var order = GetDraggingOrder();
					_idxMapper = idx => order[idx.ToArrayIndex()];
				}
				else
				{
					_idxMapper = idx => idx;
				}
			}

			foreach (var entry in _entries)
			{
				if (entry.Drag.IsDragging) continue;
				var rect = entry.GetRectTransform();
				var orgAnchor = rect.anchorMin;

				var mappedIdx = _idxMapper(entry.Idx);
				var targetAnchor = GetAnchor(mappedIdx);
				rect.SetAnchorAndAnchoredPositionToZero(Vector2.Lerp(orgAnchor, targetAnchor, lerp));
			}
		}

		private PartyIdx GetDraggigIdx()
		{
			var posX = _dragging.GetRectTransform().localPosition.x;
			var x = posX / _dragging.transform.parent.GetRectTransform().GetWidth() + 0.5f;
			
			var orgDraggingIdx = _dragging.Idx;
			PartyIdx? draggingIdx = null;

			foreach (var entry in _entries)
			{
				if (entry == _dragging)
					continue;

				var entryIdx = entry.Idx;
				var entryX = GetAnchor(entryIdx).x;

				if (entryIdx < orgDraggingIdx)
				{
					if (entryX + AlignThresholdX > x)
					{
						if (!draggingIdx.HasValue || entryIdx < draggingIdx)
							draggingIdx = entryIdx;
					}
				}
				else
				{
					if (entryX - AlignThresholdX < x)
					{
						if (!draggingIdx.HasValue || entryIdx > draggingIdx)
							draggingIdx = entryIdx;
					}
				}
			}

			if (draggingIdx == null)
				draggingIdx = _dragging.Idx;

			return draggingIdx.Value;
		}

		private PartyIdx[] GetRotatedOrder(PartyIdx idx, PartyIdx to)
		{
			var ret = new PartyIdx[_entries.Count];
			var min = idx < to ? idx : to;
			var max = idx > to ? idx : to;

			foreach (var entry in _entries)
			{
				var entryIdx = entry.Idx;
				var arrayIdx = entryIdx.ToArrayIndex();

				if (entryIdx < min || entryIdx > max)
				{
					ret[arrayIdx] = entryIdx;
				}
				else if (entryIdx != idx)
				{
					if (idx < to)
						ret[arrayIdx] = entryIdx - 1;
					else
						ret[arrayIdx] = entryIdx + 1;
				}
				else
				{
					ret[arrayIdx] = to;
				}
			}

			return ret;
		}

		private PartyIdx[] GetDraggingOrder()
		{
			Debug.Assert(_dragging);
			var orgDraggingIdx = _dragging.Idx;
			var curDraggingIdx = GetDraggigIdx();
			return GetRotatedOrder(orgDraggingIdx, curDraggingIdx);
		}

		void OnDragBegin(EntryIcon entry)
		{
			_dragging = entry;
		}

		void OnDragEnd(EntryIcon entry)
		{
			var anchor = _dragging.GetRectTransform().GetProportionalPositionToParent();
			anchor += new Vector2(0.5f, 0.5f);
			_dragging.GetRectTransform().SetAnchorAndAnchoredPositionToZero(anchor);
			_dragging = null;

			foreach (var myEntry in _entries)
				myEntry.JustSetIdx(_idxMapper(myEntry.Idx));
			Party._.Reorder(idx => _idxMapper(idx));

			_entries.Sort((a, b) => b.Idx - a.Idx);
			_idxMapper = idx => idx;
		}
	}
}
#pragma warning disable 0168
using UnityEngine;
using UnityEngine.UI;

namespace SPRPG
{
	public class CampController 
		: MonoBehaviour
	{
		[SerializeField] private ScrollRect _scrollRect;

		void Start()
		{
			var config = Config.Data.Scene.Camp;
			_scrollRect.decelerationRate = (float)config.DecelerationRate;
		}

		void Update()
		{}
	}
}
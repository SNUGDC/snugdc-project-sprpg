using UnityEngine;
using System.Collections;

namespace SPRPG
{
	public class BgmConroller : MonoBehaviour 
	{
		private static BgmConroller _instance = null;

		public static BgmConroller Instance
		{
			get{ return _instance; }
		}

		void Awake()
		{
			if (_instance != null && _instance != this) {
				Destroy (this.gameObject);
				return;
			} 

			else
				_instance = this;

			DontDestroyOnLoad (this.gameObject);
		}

		void Update()
		{
			if (Application.loadedLevelName == "Battle")
				Destroy (this.gameObject);

		}
	}
}
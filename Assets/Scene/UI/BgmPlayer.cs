using UnityEngine;
using System.Collections;

namespace SPRPG
{
	public class BgmPlayer : MonoBehaviour 
	{
		private static BgmPlayer _instance = null;
		
		public static BgmPlayer Instance
		{
			get { return _instance; }
		}
		
		void Awake()
		{
			if (_instance != null && _instance != this) 
			{
				Destroy (this.gameObject);
				return;
			} 
			
			else
				_instance = this;
						
			DontDestroyOnLoad (this.gameObject);
		}

		public void StopBgm()
		{
			Destroy (this.gameObject);
		}
	}
}
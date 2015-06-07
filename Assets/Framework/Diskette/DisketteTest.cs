#if UNITY_EDITOR

using UnityEngine;

namespace SPRPG
{
	public class DisketteTest : MonoBehaviour
	{
		public string SaveName = "test";
		public bool LoadOnStart;

		void Start()
		{
			if (LoadOnStart && !Diskette.IsLoaded)
				Diskette.Load(SaveName);
		}
	}
}

#endif
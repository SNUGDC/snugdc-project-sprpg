#if UNITY_EDITOR

using UnityEngine;

namespace SPRPG
{
	public class DisketteTest : MonoBehaviour
	{
		public string SaveName = "test";
		public bool LoadOnStart;
		public bool SaveOnDestroy;

		void Start()
		{
			if (LoadOnStart && !Diskette.IsLoaded)
				Diskette.Load(SaveName);
		}

		void OnDestroy()
		{
			if (SaveOnDestroy && Diskette.IsLoaded)
				Diskette.Save();
		}
	}
}

#endif
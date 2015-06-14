using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CampCharacterTool : MonoBehaviour
	{
		public static CampCharacterTool Current { get; private set; }

		public CharacterID ID { get; private set; }

		public static CampCharacterTool Open(CharacterID id)
		{
			CloseCurrent();
			Current = Assets._.CampCharacterTool.Instantiate();
			Current.ID = id;
			return Current;
		}

		public static void CloseCurrent()
		{
			if (Current == null) return;
			Current.Close();
			Current = null;
		}

		void Start()
		{
			Debug.Assert(Current == this);
		}

		public void Close()
		{
			Debug.Assert(Current == this);
			if (Current == this)
				Current = null;
			Destroy(gameObject);
		}

		public void OnSelectEntry()
		{
			Close();
		}

		public void OnSelectCharacterWindow()
		{
			
		}

		public void OnSelectCancel()
		{
			Close();
		}
	}
}
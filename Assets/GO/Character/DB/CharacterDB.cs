using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterDB : MonoBehaviour
	{
		[SerializeField]
		private List<CharacterData> _datas;

		public static CharacterDB _ { get; private set; }

		public static void Init(CharacterDB obj)
		{
			Debug.Assert(_ == null);
			_ = obj;
		}

#if UNITY_EDITOR
		public List<CharacterData> Edit()
		{
			return _datas;
		}
#endif

		public CharacterData Find(CharacterId id)
		{
			var ret = _datas.GetOrDefault((int) id);
			if (ret == null)
				Debug.LogWarning(LogMessages.KeyNotExists(id));
			return ret;
		}
	}
}
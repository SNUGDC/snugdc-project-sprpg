using System.Collections.Generic;
using Gem;
using UnityEngine;

namespace SPRPG
{
	public class CharacterDb : MonoBehaviour
	{
		[SerializeField]
		private List<CharacterData> _datas;

		public static CharacterDb _ { get; private set; }

		public static void Init(CharacterDb obj)
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
			var ret = _datas.GetOrDefault(id.ToIndex());
			if (ret == null)
				Debug.LogWarning(LogMessages.KeyNotExists(id));
			return ret;
		}
	}
}
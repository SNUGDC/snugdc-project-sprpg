using System.Collections.Generic;
using Gem;
using JetBrains.Annotations;
using UnityEngine;

namespace SPRPG
{
	public class CharacterDB : MonoBehaviour
	{
		[SerializeField, UsedImplicitly]
		private List<CharacterData> _datas;

		private static CharacterDB _db;

#if UNITY_EDITOR
		public List<CharacterData> Edit()
		{
			return _datas;
		}
#endif

		public static void Load(CharacterDB obj)
		{
			Debug.Assert(_db == null);
			_db = obj;
		}

		public static CharacterData Find(CharacterID id)
		{
			var ret = _db._datas.GetOrDefault((int) id);
			if (ret == null)
				Debug.LogWarning(LogMessages.KeyNotExists(id));
			return ret;
		}
	}
}
using System.Collections.Generic;
using System.Linq;
using Gem;
using LitJson;
using UnityEngine;

namespace SPRPG
{
	public class StageData
	{
		public StageId Key;
		public string Detail;
		public BossId Boss;
		public LitJson.Vector2 ButtonPosition;
	}
	
	public static class WorldDb
	{
		private static readonly List<StageData> Data;
		private static readonly Dictionary<StageId, StageData> Dict;

		static WorldDb()
		{
			Data = JsonMapper.ToObject<List<StageData>>(Assets._.WorldDb.text);
			Dict = Data.ToDictionary(stageData => stageData.Key);
		}

		public static StageData Find(StageId key)
		{
			StageData data;
			if (Dict.TryGet(key, out data))
				return data;
				
			Debug.LogError(LogMessages.KeyNotExists(key));
			return null;
		}

		public static IEnumerable<StageData> GetEnumerable()
		{
			return Data;
		}
	}
}

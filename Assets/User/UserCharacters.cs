using System.Collections.Generic;
using Gem;

namespace SPRPG
{
	public class UserCharacter
	{
		public readonly CharacterID ID;
		public readonly CharacterData Data;

		public UserCharacter(string id, SaveData.Character character)
		{
			ID = CharacterHelper.MakeID(int.Parse(id));
			Data = CharacterDB.Find(ID);
		}

		public SaveData.Character ToSaveData()
		{
			return new SaveData.Character();
		}

		public static implicit operator CharacterID(UserCharacter _this)
		{
			return _this.ID;
		}
	}

	public static class UserCharacters
	{
		private static readonly Dictionary<CharacterID, UserCharacter> _data = new Dictionary<CharacterID, UserCharacter>(CharacterConst.Count);

		public static bool Load(Dictionary<string, SaveData.Character> characters)
		{
			foreach (var kv in characters)
			{
				var value = new UserCharacter(kv.Key, kv.Value);
				_data[value.ID] = value;
			}

			return true;
		}

		public static Dictionary<string, SaveData.Character> Save()
		{
			return _data.Map((key, value) => new KeyValuePair<string, SaveData.Character>(key.ToString(), value.ToSaveData()));
		}

		public static IEnumerable<UserCharacter> GetEnumerable()
		{
			return _data.Values;
		}

		public static UserCharacter Find(CharacterID id)
		{
			UserCharacter ret;
			_data.TryGet(id, out ret);
			return ret;
		}
	}
}
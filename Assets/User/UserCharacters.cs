using System.Collections.Generic;
using System.Linq;
using Gem;

namespace SPRPG
{
	public struct UserCharacter
	{
		public CharacterID ID;

		public UserCharacter(string id, SaveData.Character character)
		{
			ID = CharacterHelper.MakeID(int.Parse(id));
		}

		public SaveData.Character ToSaveData()
		{
			return new SaveData.Character();
		}
	}

	public static class UserCharacters
	{
		private static readonly Dictionary<CharacterID, UserCharacter> _dic = new Dictionary<CharacterID, UserCharacter>(CharacterConst.Count);

		public static bool Load(Dictionary<string, SaveData.Character> characters)
		{
			foreach (var kv in characters)
			{
				var value = new UserCharacter(kv.Key, kv.Value);
				_dic[value.ID] = value;
			}

			return true;
		}

		public static Dictionary<string, SaveData.Character> Save()
		{
			return _dic.Map((key, value) => new KeyValuePair<string, SaveData.Character>(key.ToString(), value.ToSaveData()));
		}
	}
}
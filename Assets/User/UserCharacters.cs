using System.Collections.Generic;
using Gem;

namespace SPRPG
{
	public class UserCharacter
	{
		public readonly CharacterId Id;
		public readonly CharacterData Data;
		public SkillSet SkillSet;

		public UserCharacter(string id, SaveData.Character character)
		{
			Id = EnumHelper.ParseOrDefault<CharacterId>(id);
			Data = CharacterDb._.Find(Id);
			SkillSet = character.SkillSet ?? CharacterBalance._.Find(Id).SkillSetDefault.Clone();
		}

		public SaveData.Character ToSaveData()
		{
			return new SaveData.Character(SkillSet);
		}

		public static implicit operator CharacterId(UserCharacter thiz)
		{
			return thiz.Id;
		}
	}

	public static class UserCharacters
	{
		private static readonly Dictionary<CharacterId, UserCharacter> _data = new Dictionary<CharacterId, UserCharacter>(CharacterConst.Count);

		public static bool Load(Dictionary<string, SaveData.Character> characters)
		{
			foreach (var kv in characters)
			{
				var value = new UserCharacter(kv.Key, kv.Value);
				_data[value.Id] = value;
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

		public static UserCharacter Find(CharacterId id)
		{
			UserCharacter ret;
			_data.TryGet(id, out ret);
			return ret;
		}
	}
}
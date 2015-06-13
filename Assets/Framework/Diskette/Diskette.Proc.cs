using UnityEngine;

#pragma warning disable 0162

namespace SPRPG
{
	public static partial class Diskette
	{
		private static bool DoLoad(SaveData data)
		{
			do
			{
				if (!UserCharacters.Load(data.Characters))
					break;

				return true;
			} while (false);

			Debug.LogError("DoLoad failed.");
			return true;
		}

		private static SaveData DoSave()
		{
			var data = new SaveData
			{
				Characters = UserCharacters.Save()
			};
			return data;
		}
	}
}

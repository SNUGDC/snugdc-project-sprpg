#pragma warning disable 0162

using UnityEngine;

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

				if (!Party.Load(data.Party))
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
				Characters = UserCharacters.Save(),
				Party = Party.Save(),
			};
			return data;
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace SPRPG
{
	public class CharacterDescription : MonoBehaviour 
	{
		public Text Text;

		public void SetDescription(SkillKey key, CharacterId Id)
		{
			string SkillInfo = GetSkillDescription(key);
			string CharacterInfo = GetCharacterDescription(Id);
			string description = SkillInfo + CharacterInfo;
			SetDescription (description);
		}
		
		private string GetSkillDescription(SkillKey key)
		{
			return SkillBalance._.Find(key).DescriptionFormat;
		}

		private string GetCharacterDescription(CharacterId Id)
		{
			return CharacterDB._.Find(Id).name;
		}
		
		private void SetDescription(string description)
		{
			Text.text = description;
		}

	}
}
using UnityEngine;

namespace SPRPG.Profile
{
	
	public class SkillDescription : MonoBehaviour 
	{
		public void SetDescription(SkillKey key)
		{
			SkillBalance._.Find(key).Describe();
		}		
	}
}

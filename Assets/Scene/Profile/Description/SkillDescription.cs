using UnityEngine;

namespace SPRPG
{
	
	public class SkillDescription : MonoBehaviour 
	{
		public void SetDescription(SkillKey key)
		{
			SkillBalance._.Find(key).Describe();
		}		
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace SPRPG
{
	public class TextFormatter : MonoBehaviour
	{
		public bool FormatOnStart = true;

		void Start()
		{
			if (FormatOnStart)
				Format();
		}

		public void Format()
		{
			var text = gameObject.GetComponent<Text>();
			// todo: 일반화시킬 것.
			var orgText = text.text;
			var key = orgText.Substring(1, orgText.Length - 2);
			text.text = TextDictionary.Get(key);
		}
	}
}
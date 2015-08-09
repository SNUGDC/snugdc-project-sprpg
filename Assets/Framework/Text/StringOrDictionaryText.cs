namespace SPRPG
{
	public class StringOrDictionaryText
	{
		private readonly bool _isDictionaryKey;
		private readonly string _stringOrDictionaryKey;

		public StringOrDictionaryText(bool isDictionaryKey, string stringOrDictionaryKey)
		{
			_isDictionaryKey = isDictionaryKey;
			_stringOrDictionaryKey = stringOrDictionaryKey;
		}

		public string Text {
			get
			{
				if (!_isDictionaryKey) return _stringOrDictionaryKey;
				return TextDictionary.Get(_stringOrDictionaryKey);
			}
		}

		public static implicit operator string(StringOrDictionaryText thiz)
		{
			return thiz.Text;
		}
	}
}
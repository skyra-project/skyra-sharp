using System.Collections.Generic;
using System.Globalization;

namespace Skyra.Core
{
	public class CultureLanguage
	{
		public CultureLanguage(IEnumerable<string> languages)
		{
			foreach (var language in languages)
			{
				Instantiate(language);
			}
		}

		private Dictionary<string, CultureInfo> Cultures { get; } = new Dictionary<string, CultureInfo>();

		public CultureInfo this[string culture] => Cultures[culture];

		public void Instantiate(string culture)
		{
			Cultures.Add(culture, new CultureInfo(culture));
		}
	}
}

using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Skyra.Core.Utils.Urls
{
	public static class UrlRegex
	{
		private static bool Sorted { get; set; }

		[NotNull]
		public static Regex Create(bool exact = false, bool requireProtocol = true, bool tlds = true,
			bool compiled = false)
		{
			if (!Sorted)
			{
				Array.Sort(Tlds.Values, (a, b) => b.Length.CompareTo(a.Length));
				Sorted = true;
			}

			const string auth = @"(?:\S+(?::\S*)?@)?";
			const string ip = @"(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]\d|\d)(?:\.(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]\d|\d)){3}";
			const string host = @"(?:(?:[a-z\u00a1-\uffff0-9][-_]*)*[a-z\u00a1-\uffff0-9]+)";
			const string domain = @"(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*";
			const string port = @"(?::\d{2,5})?";
			const string path = @"(?:[/?#][^\s""]*)?";

			var options = compiled
				? RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled
				: RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;
			var protocol = $"(?:(?:[a-z]+:)?//){(requireProtocol ? "" : "?")}";
			var tld =
				$"(?:\\.{(tlds ? $"(?:{string.Join("|", Tlds.Values)})" : "(?:[a-z\\u00a1-\\uffff]{2,})")})\\.?";
			var regex =
				$"(?<protocol>{protocol}|www\\.){auth}(?<hostname>localhost|{ip}|{host}{domain}{tld}){port}{path}";

			return exact ? new Regex($"(?:^{regex}$)", options) : new Regex(regex, options);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Skyra.Core.Utils
{
	public static class StringExtensions
	{
		private static readonly Regex EscapeRegex = new Regex(@"[-/\\^$*+?.()|[\]{}]");

		[Pure]
		[NotNull]
		public static string Replace([NotNull] this string value, [NotNull] Regex pattern,
			Func<Group[], string> callback)
		{
			var source = value;
			var length = source.Length;
			var lastIndex = 0;

			var builder = new StringBuilder();
			foreach (var match in value.Matches(pattern))
			{
				var index = match.Index;
				builder.Append(source.Substring(lastIndex, index));
				lastIndex = index + match.Length;
				builder.Append(callback(match.Groups.Values.ToArray()));
				if (lastIndex != index) continue;
				if (lastIndex == length) break;
				builder.Append(source[lastIndex + 1]);
			}

			if (lastIndex < length) builder.Append(source.Substring(lastIndex, length - lastIndex));
			return builder.ToString();
		}

		[Pure]
		[NotNull]
		public static IEnumerable<Match> Matches([NotNull] this string value, [NotNull] Regex pattern)
		{
			return pattern.Matches(value, 0);
		}

		[Pure]
		[NotNull]
		public static string EscapeRegexPatterns([NotNull] this string value)
		{
			return value.Replace(EscapeRegex, groups => $"\\{groups[0]}");
		}
	}
}

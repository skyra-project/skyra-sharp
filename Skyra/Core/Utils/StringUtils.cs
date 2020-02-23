using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Skyra.Core.Utils
{
	public static class StringUtils
	{
		public static string Replace(this string value, Regex pattern, Func<Group[], string> callback)
		{
			var source = value;
			var length = source.Length;
			var lastIndex = 0;

			var builder = new StringBuilder();
			foreach (var match in pattern.Matches(value, 0) as IReadOnlyList<Match>)
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
	}
}

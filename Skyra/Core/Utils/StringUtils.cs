using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Skyra.Core.Utils
{
	public static class StringUtils
	{
		public static string Replace(this string value, Regex pattern, Func<Group[], string> callback)
		{
			var result = "";
			var source = value;
			var length = source.Length;
			var lastIndex = 0;
			foreach (var match in pattern.Matches(value, 0) as IReadOnlyList<Match>)
			{
				var index = match.Index;
				result += source.Substring(lastIndex, index);
				lastIndex = index + match.Length;
				result += callback(match.Groups.Values.ToArray());
				if (lastIndex != index) continue;
				if (lastIndex == length) break;
				result += source[lastIndex + 1];
			}

			if (lastIndex < length) result += source.Substring(lastIndex, length - lastIndex);
			return result;
		}
	}
}

using JetBrains.Annotations;

namespace Skyra.Core.Utils
{
	public static class Utilities
	{
		[NotNull]
		public static string CodeBlock(string language, [NotNull] string content)
		{
			var escaped = content.Replace(@"""""""", @"\""\""\""");
			return $"```{language}\n{escaped}\n```";
		}
	}
}

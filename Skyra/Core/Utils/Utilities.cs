namespace Skyra.Core.Utils
{
	public static class Utilities
	{
		public static string CodeBlock(string language, string content)
		{
			var escaped = content.Replace(@"""""""", @"\""\""\""");
			return $"```{language}\n{escaped}\n```";
		}
	}
}

namespace Skyra.Commands
{
	public class CommandAttribute : System.Attribute
	{
		public string Delimiter { get; set; } = "";
		public string? Name { get; set; } = null;
	}
}

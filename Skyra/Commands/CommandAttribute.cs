using System;

namespace Skyra.Commands
{
	public class CommandAttribute : Attribute
	{
		public string Delimiter { get; set; } = "";
		public string? Name { get; set; } = null;
	}
}

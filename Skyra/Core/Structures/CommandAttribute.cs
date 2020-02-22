using System;

namespace Skyra.Core.Structures
{
	public class CommandAttribute : Attribute
	{
		public string Delimiter { get; set; } = "";
		public string? Name { get; set; } = null;
	}
}

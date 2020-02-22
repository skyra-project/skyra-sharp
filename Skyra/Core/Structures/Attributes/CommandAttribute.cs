using System;

namespace Skyra.Core.Structures.Attributes
{
	public class CommandAttribute : Attribute
	{
		public string Delimiter { get; set; } = "";
		public string? Name { get; set; } = null;
	}
}

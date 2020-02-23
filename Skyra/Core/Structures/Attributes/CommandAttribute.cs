using System;

namespace Skyra.Core.Structures.Attributes
{
	public class CommandAttribute : Attribute
	{
		public string Delimiter { get; set; } = "";
		public string? Name { get; set; } = null;
		public bool FlagSupport { get; set; } = false;
		public bool QuotedStringSupport { get; set; } = true;
	}
}

using System;

namespace Skyra.Core.Structures.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class CommandAttribute : Attribute
	{
		public string Delimiter { get; set; } = "";
		public string? Name { get; set; } = null;
		public bool FlagSupport { get; set; }
		public bool QuotedStringSupport { get; set; } = true;
		public string[] Inhibitors { get; set; } = new string[0];
	}
}

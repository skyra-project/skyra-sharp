using System;

namespace Skyra.Core.Structures.Attributes
{
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class ArgumentAttribute : Attribute
	{
		public bool Rest { get; set; }
		public int Minimum { get; set; } = int.MinValue;
		public int Maximum { get; set; } = int.MaxValue;
		public uint MinimumValues { get; set; } = uint.MinValue;
		public uint MaximumValues { get; set; } = uint.MaxValue;
	}
}

using System;

namespace Skyra.Core.Structures.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class InhibitorAttribute : Attribute
	{
		public string? Name { get; set; } = null;
	}
}

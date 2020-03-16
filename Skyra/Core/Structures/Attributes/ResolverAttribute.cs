using System;

namespace Skyra.Core.Structures.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ResolverAttribute : Attribute
	{
		public ResolverAttribute(Type type, string displayName)
		{
			Type = type;
			DisplayName = displayName;
		}

		public string DisplayName { get; }
		public Type Type { get; }
	}
}

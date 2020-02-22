using System;

namespace Skyra.Core.Structures.Attributes
{
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

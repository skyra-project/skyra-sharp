using System;

namespace Skyra.Core.Structures.Attributes
{
	public class ResolverAttribute : Attribute
	{
		public ResolverAttribute(Type type)
		{
			Type = type;
		}

		public Type Type { get; set; }
	}
}

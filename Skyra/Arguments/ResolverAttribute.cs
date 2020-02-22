using System;

namespace Skyra.Arguments
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

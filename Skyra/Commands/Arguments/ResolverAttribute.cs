using System;

namespace Skyra.Commands.Arguments
{
	public class ResolverAttribute : Attribute
	{
		public Type Type { get; set; }

		public ResolverAttribute(Type type)
		{
			Type = type;
		}
	}
}

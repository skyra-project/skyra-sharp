using System;
using System.Reflection;

namespace Skyra.Core.Structures
{
	public struct ResolverInfo
	{
		public StructureBase Instance { get; set; }
		public MethodInfo Method { get; set; }
		public Type Type { get; set; }
		public string DisplayName { get; set; }
	}
}

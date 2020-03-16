using System;
using System.Reflection;

namespace Skyra.Core.Structures
{
	public struct ResolverInfo
	{
		internal object Instance { get; set; }
		internal MethodInfo Method { get; set; }
		internal Type Type { get; set; }
		internal string DisplayName { get; set; }
	}
}

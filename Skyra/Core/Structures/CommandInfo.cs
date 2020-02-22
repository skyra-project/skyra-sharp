using System;
using System.Reflection;

namespace Skyra.Core.Structures
{
	public struct CommandInfo
	{
		public string Delimiter { get; set; }
		public Type[] Arguments { get; set; }
		public MethodInfo Method { get; set; }
		public object Instance { get; set; }
		public string Name { get; set; }
	}
}

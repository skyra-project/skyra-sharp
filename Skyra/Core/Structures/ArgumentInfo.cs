using System;
using System.Reflection;

namespace Skyra.Core.Structures
{
	public struct ArgumentInfo
	{
		public object Instance { get; set; }
		public MethodInfo Method { get; set; }
		public Type Type { get; set; }
		public string Displayname { get; set; }
	}
}

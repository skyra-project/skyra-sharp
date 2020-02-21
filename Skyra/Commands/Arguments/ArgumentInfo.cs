using System;
using System.Reflection;

namespace Skyra.Commands.Arguments
{
	public struct ArgumentInfo
	{
		public object Instance { get; set; }
		public MethodInfo Method { get; set; }
		public Type Type { get; set; }
	}
}

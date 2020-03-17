using System;
using System.Linq;
using System.Reflection;

namespace Skyra.Core.Structures.Usage
{
	public sealed class CommandUsage
	{
		internal CommandUsage(IClient client, object instance)
		{
			Client = client;
			Overloads = GetOverloads(Client, instance.GetType());
		}

		private IClient Client { get; }
		internal CommandUsageOverload[] Overloads { get; }

		private static CommandUsageOverload[] GetOverloads(IClient client, Type instanceType)
		{
			return instanceType.GetRuntimeMethods()
				.Where(x => x.Name == "RunAsync")
				.Select(m => new CommandUsageOverload(client, m))
				.ToArray();
		}
	}
}

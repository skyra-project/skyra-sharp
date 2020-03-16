using System;
using System.Linq;
using System.Reflection;

namespace Skyra.Core.Structures.Usage
{
	public sealed class CommandUsage
	{
		internal CommandUsage(Client client, object instance)
		{
			Client = client;
			Overloads = GetOverloads(Client, instance.GetType());
		}

		private Client Client { get; }
		internal CommandUsageOverload[] Overloads { get; }

		private static CommandUsageOverload[] GetOverloads(Client client, Type instanceType)
		{
			return instanceType.GetRuntimeMethods()
				.Where(x => x.Name == "RunAsync")
				.Select(m => new CommandUsageOverload(client, m))
				.ToArray();
		}
	}
}

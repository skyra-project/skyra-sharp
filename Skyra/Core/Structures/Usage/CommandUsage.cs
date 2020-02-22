using System;
using System.Linq;
using System.Reflection;

namespace Skyra.Core.Structures.Usage
{
	public class CommandUsage
	{
		private Client Client { get; }
		public CommandUsageOverload[] Overloads { get; }

		public CommandUsage(Client client, object instance)
		{
			Client = client;
			Overloads = GetOverloads(Client, instance.GetType());
		}

		private static CommandUsageOverload[] GetOverloads(Client client, Type instanceType)
		{
			return instanceType.GetRuntimeMethods()
				.Where(x => x.Name == "RunAsync")
				.Select(m => new CommandUsageOverload(client, m))
				.ToArray();
		}
	}
}

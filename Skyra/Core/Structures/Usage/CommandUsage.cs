using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace Skyra.Core.Structures.Usage
{
	public sealed class CommandUsage
	{
		internal CommandUsage(IClient client, [NotNull] object instance)
		{
			Client = client;
			Overloads = GetOverloads(Client, instance.GetType());
		}

		private IClient Client { get; }
		public CommandUsageOverload[] Overloads { get; }

		[NotNull]
		private static CommandUsageOverload[] GetOverloads(IClient client, Type instanceType)
		{
			return instanceType.GetRuntimeMethods()
				.Where(x => x.Name == "RunAsync")
				.Select(m => new CommandUsageOverload(client, m))
				.ToArray();
		}
	}
}

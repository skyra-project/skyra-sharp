using System.Linq;
using System.Reflection;

namespace Skyra.Core.Structures.Usage
{
	public struct CommandUsageOverload
	{
		private Client Client { get; }
		public MethodBase Method { get; }
		public CommandUsageOverloadArgument[] Arguments { get; }
		public int RequiredArguments { get; }

		internal CommandUsageOverload(Client client, MethodBase methodInfo)
		{
			Client = client;
			Method = methodInfo;
			Arguments = methodInfo
				.GetParameters()
				.Skip(1)
				.Select(parameter => new CommandUsageOverloadArgument(client, parameter))
				.ToArray();
			RequiredArguments = Arguments.Count(a => !a.Optional);
		}
	}
}

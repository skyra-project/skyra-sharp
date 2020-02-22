using System.Linq;
using System.Reflection;

namespace Skyra.Core.Structures.Usage
{
	public struct CommandUsageOverload
	{
		private Client Client { get; }
		public MethodBase Method { get; }
		public CommandUsageOverloadArgument[] Arguments { get; }

		public override string ToString()
		{
			return string.Join(" ", Arguments.Select(x => x.ToString()));
		}

		internal CommandUsageOverload(Client client, MethodBase methodInfo)
		{
			Client = client;
			Method = methodInfo;
			Arguments = methodInfo
				.GetParameters()
				.Skip(1)
				.Select(parameter => new CommandUsageOverloadArgument(client, parameter))
				.ToArray();
		}
	}
}

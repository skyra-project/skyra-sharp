using System.Linq;
using System.Reflection;

namespace Skyra.Core.Structures.Usage
{
	public sealed class CommandUsageOverload
	{
		internal CommandUsageOverload(IClient client, MethodBase methodInfo)
		{
			Method = methodInfo;
			Arguments = methodInfo
				.GetParameters()
				.Skip(1)
				.Select(parameter => new CommandUsageOverloadArgument(client, parameter))
				.ToArray();
		}

		internal MethodBase Method { get; }
		internal CommandUsageOverloadArgument[] Arguments { get; }

		public override string ToString()
		{
			return string.Join(" ", Arguments.Select(x => x.ToString()));
		}
	}
}

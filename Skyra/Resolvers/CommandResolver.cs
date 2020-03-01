using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;
using Spectacles.NET.Types;

namespace Skyra.Resolvers
{
	[Resolver(typeof(CommandInfo), "command")]
	public class CommandResolver : StructureBase
	{
		public CommandResolver(Client client) : base(client)
		{
		}

		public Task<CommandInfo> ResolveAsync(Message message, CommandUsageOverloadArgument argument, string content)
		{
			if (Client.Commands.TryGetValue(content, out var resolved)) return Task.FromResult(resolved);
			throw new ArgumentException($"I could not resolve a command from {content}");
		}
	}
}

using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;

namespace Skyra.Resolvers
{
	[Resolver(typeof(CommandInfo), "command")]
	public class CommandResolver : StructureBase
	{
		public CommandResolver(IClient client) : base(client)
		{
		}

		public Task<CommandInfo> ResolveAsync(CoreMessage message, CommandUsageOverloadArgument argument,
			string content)
		{
			if (Client.Commands.TryGetValue(content, out var resolved)) return Task.FromResult(resolved);
			return Task.FromException<CommandInfo>(
				new ArgumentException($"I could not resolve a command from {content}"));
		}
	}
}

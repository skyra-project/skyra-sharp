using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;
using Spectacles.NET.Types;

namespace Skyra.Arguments
{
	[Resolver(typeof(CommandInfo), "command")]
	public class CommandResolver : StructureBase
	{
		public CommandResolver(Client client) : base(client)
		{
		}

		public Task<CommandInfo> ResolveAsync(Message message, CommandUsageOverloadArgument argument, string content)
		{
			var command = Client.Commands[content];
			if (string.IsNullOrEmpty(command.Name)) throw new Exception("Gimme a valid command!");
			return Task.FromResult(command);
		}
	}
}

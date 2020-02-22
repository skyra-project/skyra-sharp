using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Arguments
{
	[Resolver(typeof(CommandInfo), "command")]
	public class CommandResolver
	{
		private readonly Client _client;

		public CommandResolver(Client client)
		{
			_client = client;
		}

		public Task<CommandInfo> ResolveAsync(Message _, string content)
		{
			var command = _client.Commands[content];
			if (string.IsNullOrEmpty(command.Name)) throw new Exception("Gimme a valid command!");
			return Task.FromResult(command);
		}
	}
}

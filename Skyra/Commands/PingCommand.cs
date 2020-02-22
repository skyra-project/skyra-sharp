using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Utils;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	[Command]
	public class PingCommand
	{
		private readonly Client _client;

		public PingCommand(Client client)
		{
			_client = client;
		}

		public async Task RunAsync(Message message)
		{
			var response = await message.SendAsync(_client, "Ping...");
			await response.EditAsync(_client, $"Pong! Took {Difference(message, response).Milliseconds.ToString()}ms.");
		}

		private static TimeSpan Difference(Message message, Message response)
		{
			return (response.EditedTimestamp ?? response.Timestamp) - (message.EditedTimestamp ?? message.Timestamp);
		}
	}
}

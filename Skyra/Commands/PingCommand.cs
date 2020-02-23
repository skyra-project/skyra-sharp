using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Utils;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	[Command]
	public class PingCommand : StructureBase
	{
		public PingCommand(Client client) : base(client)
		{
		}

		public async Task RunAsync(Message message)
		{
			var response = await message.SendAsync(Client, "Ping...");
			await response.EditAsync(Client, $"Pong! Took {Difference(message, response).Milliseconds.ToString()}ms.");
		}

		private static TimeSpan Difference(Message message, Message response)
		{
			return (response.EditedTimestamp ?? response.Timestamp) - (message.EditedTimestamp ?? message.Timestamp);
		}
	}
}

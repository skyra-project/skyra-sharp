using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Commands
{
	[Command]
	public class PingCommand : StructureBase
	{
		public PingCommand(Client client) : base(client)
		{
		}

		public async Task RunAsync(CoreMessage message)
		{
			var response = await message.SendAsync(Client, "Ping...");
			await response.EditAsync(Client, $"Pong! Took {Difference(message, response).Milliseconds.ToString()}ms.");
		}

		private static TimeSpan Difference(CoreMessage message, CoreMessage response)
		{
			return (response.EditedTimestamp ?? response.Timestamp) - (message.EditedTimestamp ?? message.Timestamp);
		}
	}
}

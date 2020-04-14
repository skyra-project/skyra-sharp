using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Worker.Commands
{
	[Command]
	public class PingCommand : StructureBase
	{
		public PingCommand(IClient client) : base(client)
		{
		}

		public async Task RunAsync([NotNull] CoreMessage message)
		{
			var response = await message.SendLocaleAsync("Ping");
			await response.EditLocaleAsync("Pong", Difference(message, response).Milliseconds);
		}

		private static TimeSpan Difference([NotNull] CoreMessage message, [NotNull] CoreMessage response)
		{
			return (response.EditedTimestamp ?? response.Timestamp) - (message.EditedTimestamp ?? message.Timestamp);
		}
	}
}

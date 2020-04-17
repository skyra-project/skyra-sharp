using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Worker.Events.Raw
{
	[Event]
	public class RawMessageCreateEvent : StructureBase
	{
		public RawMessageCreateEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawMessageCreateAsync += RunAsync;
		}

		private async Task RunAsync([NotNull] Message rawMessage)
		{
			// Instantiate CoreMessage for usage everywhere
			var message = Core.Cache.Models.Message.From(Client, rawMessage);
			await message.CacheAsync();

			// Handle the message
			await Client.EventHandler.OnMessageCreateAsync(message);
		}
	}
}

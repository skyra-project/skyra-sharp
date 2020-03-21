using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events.Raw
{
	[Event]
	public class RawMessageCreateEvent : StructureBase
	{
		public RawMessageCreateEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawMessageCreateAsync += RunAsync;
		}

		private async Task RunAsync(Message rawMessage)
		{
			var message = CoreMessage.From(Client, rawMessage);
			await message.CacheAsync();
			await Client.EventHandler.OnMessageCreateAsync(message);
		}
	}
}

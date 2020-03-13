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
		public RawMessageCreateEvent(Client client) : base(client)
		{
			Client.EventHandler.OnRawMessageCreate += Run;
		}

		private void Run(Message message)
		{
			Task.Run(() => RunMonitors(CoreMessage.From(message)));
		}

		private async Task RunMonitors(CoreMessage message)
		{
			await message.CacheAsync(Client);
			Client.EventHandler.OnMessageCreate(message);
		}
	}
}

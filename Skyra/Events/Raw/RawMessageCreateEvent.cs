using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Models.Prompts;
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
			// Instantiate CoreMessage for usage everywhere
			var message = CoreMessage.From(Client, rawMessage);
			await message.CacheAsync();

			// Handle the message
			await Client.EventHandler.OnMessageCreateAsync(message);

			// Handle prompts
			// TODO(kyranet): Handle deletion (from monitors, commands, etc)
			// TODO(kyranet): Handle commands, they must not run prompts.
			var prompt = await Client.Cache.Prompts.GetAsync(CorePromptStateMessage.ToKey(message));
			if (!(prompt is null)) await Client.EventHandler.OnRawPromptAsync(prompt, message);
		}
	}
}

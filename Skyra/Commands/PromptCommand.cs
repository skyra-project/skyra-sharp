using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Commands
{
	[Command]
	public class PromptCommand : StructureBase
	{
		public PromptCommand(IClient client) : base(client)
		{
		}

		public async Task RunAsync(CoreMessage message)
		{
			var state = new CorePromptStateMessage(message.AuthorId, message.ChannelId, message.Content);
			var prompt = new CorePromptState(Client, CorePromptStateType.MessageSingleUser, state);
			await Client.Cache.Prompts.SetAsync(prompt, TimeSpan.FromMinutes(5));

			await message.SendAsync("Say something!");
		}
	}
}

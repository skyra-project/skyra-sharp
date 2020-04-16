using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Worker.Commands
{
	[Command]
	public sealed class PromptCommand : StructureBase
	{
		public PromptCommand(IClient client) : base(client)
		{
		}

		public async Task RunAsync([NotNull] CoreMessage message, uint page = 0)
		{
			var richDisplay = new CoreRichDisplay(message.AuthorId, message.Id);
			foreach (var (key, value) in Client.Commands)
			{
				richDisplay.AddPage(embed => embed.SetTitle(key).SetDescription(value.Name));
			}

			await richDisplay.RunAsync(message, new CoreRichDisplayRunOptions
			{
				StartPage = page
			});
		}
	}
}

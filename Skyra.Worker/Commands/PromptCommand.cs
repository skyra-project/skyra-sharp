using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;
using Message = Skyra.Core.Cache.Models.Message;

namespace Skyra.Worker.Commands
{
	[Command]
	public sealed class PromptCommand : StructureBase
	{
		public PromptCommand(IClient client) : base(client)
		{
		}

		public async Task RunAsync([NotNull] Message message, [Argument(Minimum = 1)] int page = 1)
		{
			var richDisplay = new RichDisplay(message.AuthorId, message.Id);
			foreach (var (key, value) in Client.Commands)
			{
				richDisplay.AddPage(embed => embed.SetTitle(key).SetDescription(value.Name));
			}

			await richDisplay.SetUpAsync(message, new RichDisplayRunOptions
			{
				StartPage = page - 1,
				MessageContent = new SendableMessage
				{
					Content = "Loading..."
				}
			});
		}
	}
}

using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Worker.Extensions;

namespace Skyra.Worker.Commands
{
	[Command(Delimiter = " ")]
	public sealed class HelpCommand : StructureBase
	{
		public HelpCommand(IClient client) : base(client)
		{
		}

		public async Task RunAsync([NotNull] Message message, CommandInfo command)
		{
			await message.SendLocaleAsync(@"UsageCommand", command.Name, GetUsage(command));
		}

		[NotNull]
		private static string GetUsage(CommandInfo command)
		{
			return string.Join("\n", command.Usage.Overloads.Select(o => $"- `Skyra.Worker, {command.Name} {o}`"));
		}
	}
}

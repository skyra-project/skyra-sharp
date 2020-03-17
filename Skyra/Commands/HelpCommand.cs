using System.Linq;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Commands
{
	[Command(Delimiter = " ")]
	public class HelpCommand : StructureBase
	{
		public HelpCommand(IClient client) : base(client)
		{
		}

		public async Task RunAsync(CoreMessage message, CommandInfo command)
		{
			await message.SendLocaleAsync(Client, "UsageCommand", new object?[] {command.Name, GetUsage(command)});
		}

		private static string GetUsage(CommandInfo command)
		{
			return string.Join("\n", command.Usage.Overloads.Select(o => $"- `Skyra, {command.Name} {o}`"));
		}
	}
}

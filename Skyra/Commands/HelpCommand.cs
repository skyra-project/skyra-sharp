using System.Linq;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Utils;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	[Command(Delimiter = " ")]
	public class HelpCommand : StructureBase
	{
		public HelpCommand(Client client) : base(client)
		{
		}

		public async Task RunAsync(Message message, CommandInfo command)
		{
			await message.SendAsync(Client,
				$"The usage(s) for {command.Name} are:\n{GetUsage(command)}");
		}

		private static string GetUsage(CommandInfo command)
		{
			return string.Join("\n", command.Usage.Overloads.Select(o => $"- `Skyra, {command.Name} {o}`"));
		}
	}
}

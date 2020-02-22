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
	public class HelpCommand
	{
		private readonly Client _client;

		public HelpCommand(Client client)
		{
			_client = client;
		}

		public async Task RunAsync(Message message, CommandInfo command)
		{
			await message.SendAsync(_client,
				$"The usage(s) for {command.Name} are: {GetUsage(command)}.");
		}

		private static string GetUsage(CommandInfo command)
		{
			return string.Join(", ", command.Usage.Overloads.Select(o => $"`Skyra, {command.Name} {o}`"));
		}
	}
}

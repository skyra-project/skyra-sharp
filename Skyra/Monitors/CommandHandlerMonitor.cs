using System;
using System.Reflection;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;
using Skyra.Core.Utils;
using Spectacles.NET.Types;

namespace Skyra.Monitors
{
	[Monitor(IgnoreOthers = false, IgnoreEdits = false)]
	public class CommandHandlerMonitor : StructureBase
	{
		public CommandHandlerMonitor(Client client) : base(client)
		{
		}

		public async Task RunAsync(Message message)
		{
			const string prefix = "t!";
			var prefixLess = message.Content.Replace(prefix, "");

			var commandName = prefixLess.Contains(" ")
				? prefixLess.Substring(0, prefixLess.IndexOf(" ", StringComparison.Ordinal))
				: prefixLess;
			var command = Client.Commands[commandName.ToLower()];
			if (command.Name == string.Empty) return;

			await RunArgumentsAsync(message, command, prefixLess.Substring(commandName.Length));
		}

		private async Task RunArgumentsAsync(Message message, CommandInfo command, string content)
		{
			var parser = new CommandUsageParser(command, message, content);
			try
			{
				parser.Run();
				await RunCommandAsync(message, command, parser);
			}
			catch (ArgumentException exception)
			{
				await message.SendAsync(Client,
					$"Argument Error: {exception.InnerException?.Message ?? exception.Message}");
			}
			catch (Exception exception)
			{
				Console.Error.WriteLine($"[COMMANDS]: {exception.Message}\n{exception.StackTrace}");
				await message.SendAsync(Client, "Whoops! Something while processing arguments!");
			}
		}

		private async Task RunCommandAsync(Message message, CommandInfo command, CommandUsageParser parser)
		{
			try
			{
#pragma warning disable CS8600, CS8602
				await (Task) parser.Overload!.Method.Invoke(command.Instance, parser.Parameters);
#pragma warning restore CS8600, CS8602
			}
			catch (TargetInvocationException exception)
			{
				if (exception.InnerException == null) throw;
				Console.Error.WriteLine($"[COMMANDS]: {exception.Message}\n{exception.StackTrace}");
				await message.SendAsync(Client, "Whoops! Something happened!");
			}
			catch (Exception exception)
			{
				Console.Error.WriteLine($"[COMMANDS]: {exception.Message}\n{exception.StackTrace}");
				await message.SendAsync(Client, "Whoops! Something happened while processing the command!");
			}
		}
	}
}

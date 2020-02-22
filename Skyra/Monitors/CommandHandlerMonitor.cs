using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;
using Skyra.Core.Utils;
using Spectacles.NET.Types;

namespace Skyra.Monitors
{
	[Monitor(IgnoreOthers = false, IgnoreEdits = false)]
	public class CommandHandlerMonitor
	{
		private readonly Client _client;

		public CommandHandlerMonitor(Client client)
		{
			_client = client;
		}

		public async Task RunAsync(Message message)
		{
			const string prefix = "t!";
			var prefixLess = message.Content.Replace(prefix, "");

			var commandName = prefixLess.Contains(" ")
				? prefixLess.Substring(0, prefixLess.IndexOf(" ", StringComparison.Ordinal))
				: prefixLess;
			var command = _client.Commands[commandName.ToLower()];
			if (command.Name == string.Empty) return;

			var parser = new TextPrompt(command, message, prefixLess.Substring(commandName.Length));
			try
			{
				await parser.RunAsync();
			}
			catch (ArgumentException exception)
			{
				await message.SendAsync(_client,
					$"Argument Error: {exception.InnerException?.Message ?? exception.Message}");
				return;
			}
			catch (Exception exception)
			{
				Console.Error.WriteLine($"[COMMANDS]: {exception.Message}\n{exception.StackTrace}");
				await message.SendAsync(_client, "Whoops! Something happened!");
				return;
			}

			try
			{
				await (Task) parser.Overload.Method.Invoke(command.Instance, parser.Parameters);
			}
			catch (TargetInvocationException exception)
			{
				if (exception.InnerException == null) throw;
				Console.Error.WriteLine($"[COMMANDS]: {exception.Message}\n{exception.StackTrace}");
				await message.SendAsync(_client, "Whoops! Something happened!");
			}
			catch (Exception exception)
			{
				Console.Error.WriteLine($"[COMMANDS]: {exception.Message}\n{exception.StackTrace}");
				await message.SendAsync(_client, "Whoops! Something happened!");
			}
		}
	}
}

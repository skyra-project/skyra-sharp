using System;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures.Attributes;
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
			var args = new object[command.Arguments.Length + 1];
			args[0] = message;

			if (command.Arguments.Any())
			{
				var replaced = prefixLess.Replace(commandName, "");
				var trimmed = replaced.Trim();
				var split = trimmed.Split(command.Delimiter);

				for (var i = 0; i < command.Arguments.Length; i++)
				{
					var resolver = _client.Resolvers[command.Arguments[i]];
					var resolved =
						(object) ((dynamic) resolver.Method.Invoke(resolver.Instance, new object[] {message, split[i]}))
						.Result;
					args[i + 1] = resolved;
				}
			}

			try
			{
				await (Task) command.Method.Invoke(command.Instance, args);
			}
			catch (Exception exception)
			{
				Console.Error.WriteLine($"[COMMANDS]: {exception.Message}\n{exception.StackTrace}");
			}
		}
	}
}

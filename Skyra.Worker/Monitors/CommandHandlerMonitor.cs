using System;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Database;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Base;
using Skyra.Core.Structures.Exceptions;
using Skyra.Core.Structures.Usage;

namespace Skyra.Worker.Monitors
{
	[Monitor(IgnoreOthers = false, IgnoreEdits = false)]
	public class CommandHandlerMonitor : StructureBase, IMonitor
	{
		private const string DefaultPrefix = "t!";

		public CommandHandlerMonitor(IClient client) : base(client)
		{
		}

		public async Task RunAsync([NotNull] CoreMessage message)
		{
			if (!await RunCommandParsingAsync(message))
			{
				await RunPromptAsync(message);
			}
		}

		private async Task RunPromptAsync([NotNull] CoreMessage message)
		{
			var key = CorePromptStateMessage.ToKey(message);
			var result = await Client.Cache.Prompts.GetAsync(key);
			if (result is null) return;

			var state = (result.State as CorePromptStateMessage)!;
			// ReSharper disable once PossibleNullReferenceException
			await state.RunAsync(message, state);
			await Client.Cache.Prompts.DeleteAsync(key);
		}

		private async Task<bool> RunCommandParsingAsync([NotNull] CoreMessage message)
		{
			var (prefix, typeResult) = await GetPrefixAsync(message);
			if (typeResult == PrefixTypeResult.None) return false;

			var prefixLess = message.Content.Substring(prefix!.Length).TrimStart();
			if (prefixLess == "" && typeResult == PrefixTypeResult.MentionPrefix)
			{
				await message.SendAsync(message.GuildId == null
					? $"The prefix for my commands is `{DefaultPrefix}`"
					: $"The prefix for my commands in this server is `{await RetrieveGuildPrefixAsync((ulong) message.GuildId)}`");
				return true;
			}

			var commandName = GetCommandName(prefixLess);
			if (Client.Commands.TryGetValue(commandName.ToLower(), out var command))
			{
				await RunInhibitorsAsync(message, command, prefixLess.Substring(commandName.Length));
				return true;
			}

			await Client.EventHandler.OnCommandUnknownAsync(message, commandName);
			return false;
		}

		private async Task RunInhibitorsAsync(CoreMessage message, CommandInfo command, string content)
		{
			foreach (var inhibitor in command.Inhibitors)
			{
				try
				{
					if (await inhibitor.RunAsync(message, command)) return;
				}
				catch (InhibitorException exception)
				{
					await Client.EventHandler.OnCommandInhibitedAsync(message, command.Name, exception);
					return;
				}
				catch (Exception exception)
				{
					await Client.EventHandler.OnInhibitorExceptionAsync(message, command.Name, exception);
					return;
				}
			}

			await RunArgumentsAsync(message, command, content);
		}

		private async Task RunArgumentsAsync(CoreMessage message, CommandInfo command, string content)
		{
			var parser = new CommandUsageParser(command, message, content);
			try
			{
				parser.Run();
				await RunCommandAsync(message, command, parser);
			}
			catch (ArgumentException exception)
			{
				await Client.EventHandler.OnCommandArgumentExceptionAsync(message, command.Name, exception);
			}
			catch (Exception exception)
			{
				await Client.EventHandler.OnArgumentErrorAsync(message, command.Name, exception);
			}
		}

		private async Task RunCommandAsync(CoreMessage message, CommandInfo command,
			[NotNull] CommandUsageParser parser)
		{
			try
			{
				await Client.EventHandler.OnCommandRunAsync(message, command.Name, parser.Parameters);
#pragma warning disable CS8600, CS8602
				await (Task) parser.Overload!.Method.Invoke(command.Instance, parser.Parameters);
#pragma warning restore CS8600, CS8602
				await Client.EventHandler.OnCommandSuccessAsync(message, command.Name, parser.Parameters);
			}
			catch (Exception exception)
			{
				await Client.EventHandler.OnCommandErrorAsync(message, command.Name, parser.Parameters, exception);
			}
		}

		private async Task<(string?, PrefixTypeResult)> GetPrefixAsync(CoreMessage message)
		{
			var mentionPrefix = GetMentionPrefix(message);
			return mentionPrefix.Item2 == PrefixTypeResult.None
				? message.GuildId == null
					? GetDefaultPrefix(message)
					: await GetGuildPrefixAsync(message)
				: mentionPrefix;
		}

		private (string?, PrefixTypeResult) GetMentionPrefix([NotNull] CoreMessage message)
		{
			// If the content is shorter than the minimum characters needed for a mention prefix, skip
			if (message.Content.Length < 20)
			{
				return (null, PrefixTypeResult.None);
			}

			int prefixLength;
			if (message.Content.StartsWith("<@!", StringComparison.Ordinal))
			{
				prefixLength = 3;
			}
			else if (message.Content.StartsWith("<@", StringComparison.Ordinal))
			{
				prefixLength = 2;
			}
			else
			{
				return (null, PrefixTypeResult.None);
			}

			return message.Content.Substring(prefixLength).StartsWith($"{Client.Id}>", StringComparison.Ordinal)
				? (message.Content.Substring(0, prefixLength + Client.Id.ToString()!.Length + 1),
					PrefixTypeResult.MentionPrefix)
				: ((string?) null, PrefixTypeResult.None);
		}

		private static async Task<(string?, PrefixTypeResult)> GetGuildPrefixAsync(
			[NotNull] CoreMessage message)
		{
			Debug.Assert(message.GuildId != null, "message.GuildId != null");
			var prefix = await RetrieveGuildPrefixAsync((ulong) message.GuildId);
			return message.Content.StartsWith(prefix, StringComparison.Ordinal)
				? (prefix, PrefixTypeResult.RegularPrefix)
				: ((string?) null, PrefixTypeResult.None);
		}

		private static (string?, PrefixTypeResult) GetDefaultPrefix([NotNull] CoreMessage message)
		{
			const string prefix = DefaultPrefix;
			return message.Content.StartsWith(prefix, StringComparison.Ordinal)
				? (prefix, PrefixTypeResult.RegularPrefix)
				: ((string?) null, PrefixTypeResult.None);
		}

		[ItemNotNull]
		private static async Task<string> RetrieveGuildPrefixAsync(ulong guildId)
		{
			await using var db = new SkyraDatabaseContext();
			return (await db.Guilds.FindAsync(guildId))?.Prefix ?? DefaultPrefix;
		}

		[NotNull]
		private static string GetCommandName([NotNull] string content)
		{
			var index = content.IndexOf(" ", StringComparison.Ordinal);
			return index == -1 ? content : content.Substring(0, index);
		}

		private enum PrefixTypeResult
		{
			None,
			MentionPrefix,
			RegularPrefix
		}
	}
}

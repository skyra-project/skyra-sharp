﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Database;
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
		private const string DefaultPrefix = "t!";

		public CommandHandlerMonitor(Client client) : base(client)
		{
		}

		public async Task RunAsync(Message message)
		{
			var (prefix, typeResult) = await GetPrefixAsync(message);
			if (typeResult == PrefixTypeResult.None) return;

			var prefixLess = message.Content.Substring(prefix!.Length).TrimStart();
			if (prefixLess == "" && typeResult == PrefixTypeResult.MentionPrefix)
			{
				await message.SendAsync(Client, message.GuildId == null
					? $"The prefix for my commands is `{DefaultPrefix}`"
					: $"The prefix for my commands in this server is `{await RetrieveGuildPrefixAsync(ulong.Parse(message.GuildId))}`");
				return;
			}

			var commandName = GetCommandName(prefixLess);
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

		private async Task<(string?, PrefixTypeResult)> GetPrefixAsync(Message message)
		{
			var mentionPrefix = await GetMentionPrefixAsync(message);
			return mentionPrefix.Item2 == PrefixTypeResult.None
				? message.GuildId == null
					? GetDefaultPrefix(message)
					: await GetGuildPrefixAsync(message)
				: mentionPrefix;
		}

		private async Task<(string?, PrefixTypeResult)> GetMentionPrefixAsync(Message message)
		{
			// If the content is shorter than the minimum characters needed for a mention prefix, skip
			if (message.Content.Length < 20)
			{
				return (null, PrefixTypeResult.None);
			}

			int prefixLength;
			if (message.Content.StartsWith("<@!"))
			{
				prefixLength = 3;
			}
			else if (message.Content.StartsWith("<@"))
			{
				prefixLength = 2;
			}
			else
			{
				return (null, PrefixTypeResult.None);
			}

			var clientId = await Client.Cache.GetClientUser();
			return message.Content.Substring(prefixLength).StartsWith($"{clientId}>")
				? (message.Content.Substring(0, prefixLength + clientId.Length + 1), PrefixTypeResult.MentionPrefix)
				: (null, PrefixTypeResult.None);
		}

		private static async Task<(string?, PrefixTypeResult)> GetGuildPrefixAsync(Message message)
		{
			var prefix = await RetrieveGuildPrefixAsync(ulong.Parse(message.GuildId));
			return message.Content.StartsWith(prefix)
				? (prefix, PrefixTypeResult.RegularPrefix)
				: (null, PrefixTypeResult.None);
		}

		private static (string?, PrefixTypeResult) GetDefaultPrefix(Message message)
		{
			const string prefix = DefaultPrefix;
			return message.Content.StartsWith(prefix)
				? (prefix, PrefixTypeResult.RegularPrefix)
				: (null, PrefixTypeResult.None);
		}

		private static async Task<string> RetrieveGuildPrefixAsync(ulong guildId)
		{
			await using var db = new SkyraDatabaseContext();
			return (await db.Guilds.FindAsync(guildId))?.Prefix ?? DefaultPrefix;
		}

		private static string GetCommandName(string content)
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

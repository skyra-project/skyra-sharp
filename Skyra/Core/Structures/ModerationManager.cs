using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Skyra.Core.Cache.Models;
using Skyra.Core.Database;
using Skyra.Core.Database.Models;
using Spectacles.NET.Rest.APIError;
using Spectacles.NET.Types;

namespace Skyra.Core.Structures
{
	public sealed class ModerationManager
	{
		public ModerationManager(IClient client, ulong guildId)
		{
			Client = client;
			GuildId = guildId;
		}

		private IClient Client { get; }
		private ulong GuildId { get; }

		[Pure]
		public async Task<ulong?> GetModerationLogsChannelAsync()
		{
			await using var db = new SkyraDatabaseContext();
			var guild = await db.Guilds.FindAsync(GuildId);
			return guild?.Channel.ModerationLogs;
		}

		[Pure]
		[ItemNotNull]
		public async Task<IEnumerable<CoreMessage>> GetModerationLogsMessagesAsync(uint retryTimes = 5)
		{
			var channelId = await GetModerationLogsChannelAsync();
			if (channelId == null) return new CoreMessage[0];

			try
			{
				var result = await Client.Rest.Channels[channelId.ToString()].Messages.GetAsync<Message[]>(
					new Dictionary<string, string>
						{{"limit", "100"}});
				return result.Select(v => CoreMessage.From(Client, v));
			}
			catch (DiscordAPIException)
			{
				throw;
			}
			catch (Exception)
			{
				if (retryTimes == 0) throw;
				return await GetModerationLogsMessagesAsync(retryTimes - 1);
			}
		}

		[Pure]
		[ItemCanBeNull]
		public async Task<Moderation?> GetLatestModerationEntryByUser(ulong userId)
		{
			var minimumTime = DateTime.Now.Subtract(TimeSpan.FromSeconds(15));
			await using var db = new SkyraDatabaseContext();
			return await db.Moderation.Where(entry => entry.GuildId == GuildId
			                                          && entry.UserId == userId
			                                          && entry.CreatedAt >= minimumTime)
				.OrderByDescending(entry => entry.CreatedAt)
				.FirstAsync();
		}

		[Pure]
		[ItemCanBeNull]
		public async Task<Moderation?> GetAsync(uint caseId)
		{
			await using var db = new SkyraDatabaseContext();
			return await db.Moderation.FindAsync(GuildId, caseId);
		}

		[Pure]
		[ItemNotNull]
		public async Task<Moderation[]> GetAsync(IEnumerable<uint> caseIds)
		{
			await using var db = new SkyraDatabaseContext();
			return await db.Moderation.Where(entry => entry.GuildId == GuildId && caseIds.Contains(entry.CaseId))
				.OrderBy(entry => entry.CaseId)
				.ToArrayAsync();
		}

		[Pure]
		[ItemNotNull]
		public async Task<Moderation[]> GetAsync(ulong userId)
		{
			await using var db = new SkyraDatabaseContext();
			return await db.Moderation.Where(entry => entry.GuildId == GuildId && entry.UserId == userId)
				.OrderBy(entry => entry.CaseId)
				.ToArrayAsync();
		}

		[Pure]
		[ItemNotNull]
		public async Task<Moderation[]> GetAsync()
		{
			await using var db = new SkyraDatabaseContext();
			return await db.Moderation.Where(entry => entry.GuildId == GuildId)
				.OrderBy(entry => entry.CaseId)
				.ToArrayAsync();
		}

		[Pure]
		[ItemNotNull]
		public async Task<Moderation> Create([NotNull] Moderation moderation)
		{
			await using var db = new SkyraDatabaseContext();
			moderation.GuildId = GuildId;
			moderation.CaseId = (uint) await db.Moderation.CountAsync(entry => entry.GuildId == GuildId) + 1;
			await db.Moderation.AddAsync(moderation);
			await db.SaveChangesAsync();
			return moderation;
		}
	}
}

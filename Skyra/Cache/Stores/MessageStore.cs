using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skyra.Cache.Models;
using Spectacles.NET.Types;

namespace Skyra.Cache.Stores
{
	public class MessageStore : CacheStore<CachedMessage>
	{
		public MessageStore(CacheClient client) : base(client, "messages")
		{
		}

		public new async Task<CachedMessage?> GetAsync(string id, string? parent = null)
		{
			var result = await Database.StringGetAsync($"{FormatKeyName(parent)}:{id}");
			return !result.IsNull ? JsonConvert.DeserializeObject<CachedMessage>(result) : null;
		}

		public Task SetAsync(Message entry, string? parent = null)
			=> Task.WhenAll(
				Client.Users.SetAsync(entry.Author),
				entry.Member == null
					? Task.FromResult(false)
					: Client.Members.SetAsync(new CachedGuildMember(entry.Member)),
				SetAsync(new CachedMessage(entry), parent));

		public override async Task SetAsync(CachedMessage entry, string? parent = null)
		{
			var id = $"{FormatKeyName(parent)}:{entry.Id}";
			await Database.SetAddAsync(id, SerializeValue(entry));
			await Database.KeyExpireAsync(id, TimeSpan.FromMinutes(20));
		}

		public override Task SetAsync(IEnumerable<CachedMessage> entries, string? parent = null)
			=> Task.WhenAll(entries.Select(entry => SetAsync(entry, parent)));
	}
}

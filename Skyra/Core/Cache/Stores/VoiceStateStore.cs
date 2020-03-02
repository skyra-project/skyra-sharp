using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores
{
	public class VoiceStateStore : CacheStore<CoreVoiceState>
	{
		public VoiceStateStore(CacheClient client) : base(client, "voiceStates")
		{
		}

		public async Task SetAsync(VoiceState entry, string? parent = null)
		{
			await SetAsync(new CoreVoiceState(entry), parent);
		}

		public override Task SetAsync(CoreVoiceState entry, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent),
				new[] {new HashEntry(entry.UserId, SerializeValue(entry))});
		}

		public async Task SetAsync(IEnumerable<VoiceState> entries, string? parent = null)
		{
			await SetAsync(entries.Select(e => new CoreVoiceState(e)), parent);
		}

		public override Task SetAsync(IEnumerable<CoreVoiceState> entries, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(entry.UserId, SerializeValue(entry))).ToArray());
		}
	}
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public class VoiceStateStore : CacheStore<VoiceState>
	{
		public VoiceStateStore(CacheClient client) : base(client, "voiceStates")
		{
		}

		public override async Task SetAsync(VoiceState entry, string? parent = null)
		{
			await Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.UserId, SerializeValue(entry))});
		}

		public override async Task SetAsync(IEnumerable<VoiceState> entries, string? parent = null)
		{
			await Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(entry.UserId, SerializeValue(entry))).ToArray());
		}
	}
}

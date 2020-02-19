using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Cache.Models;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public class EditableMessagesStore : CacheStore<CachedEditableMessage>
	{
		public EditableMessagesStore(CacheClient client) : base(client, "editable_messages")
		{
		}

		public override Task SetAsync(CachedEditableMessage entry, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public override Task SetAsync(IEnumerable<CachedEditableMessage> entries, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}

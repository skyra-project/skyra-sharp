using System.Collections.Generic;
using System.Threading.Tasks;
using Spectacles.NET.Types;

namespace Skyra.Cache.Stores
{
	public class MessageStore : CacheStore<Message>
	{
		public MessageStore(CacheClient client) : base(client, "messages")
		{
		}

		public override Task SetAsync(Message entry, string? parent = null)
		{
			throw new System.NotImplementedException();
		}

		public override Task SetAsync(IEnumerable<Message> entries, string? parent = null)
		{
			throw new System.NotImplementedException();
		}
	}
}

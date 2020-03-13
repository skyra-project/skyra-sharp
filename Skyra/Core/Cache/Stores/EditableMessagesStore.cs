using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class EditableMessagesStore : HashMapCacheStoreBase<CoreEditableMessage>
	{
		internal EditableMessagesStore(CacheClient client) : base(client, "editable_messages")
		{
		}

		protected override string GetKey(CoreEditableMessage value)
		{
			return value.Id.ToString();
		}
	}
}

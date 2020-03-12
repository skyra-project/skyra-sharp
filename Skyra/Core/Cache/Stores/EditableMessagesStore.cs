using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public class EditableMessagesStore : HashMapCacheStoreBase<CoreEditableMessage>
	{
		public EditableMessagesStore(CacheClient client) : base(client, "editable_messages")
		{
		}

		public override string GetKey(CoreEditableMessage value)
		{
			return value.Id.ToString();
		}
	}
}

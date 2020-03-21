using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class EditableMessagesStore : HashMapCacheStoreBase<CoreEditableMessage>
	{
		internal EditableMessagesStore(CacheClient context) : base(context, "editable_messages")
		{
		}

		protected override string GetKey(CoreEditableMessage value)
		{
			return value.Id.ToString();
		}
	}
}

using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class EditableMessagesStore : HashMapCacheStoreBase<CoreEditableMessage>
	{
		internal EditableMessagesStore(CacheClient context) : base(context, "editable_messages")
		{
		}

		[NotNull]
		protected override string GetKey([NotNull] CoreEditableMessage value)
		{
			return value.Id.ToString();
		}
	}
}

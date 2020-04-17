using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class EditableMessagesStore : HashMapCacheStoreBase<EditableMessage>
	{
		internal EditableMessagesStore(CacheClient context) : base(context, "editable_messages")
		{
		}

		[NotNull]
		protected override string GetKey([NotNull] EditableMessage value)
		{
			return value.Id.ToString();
		}
	}
}

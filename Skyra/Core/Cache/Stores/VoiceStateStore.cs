using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class VoiceStateStore : HashMapCacheStoreBase<CoreVoiceState>
	{
		internal VoiceStateStore(CacheClient context) : base(context, "voiceStates")
		{
		}

		protected override string GetKey(CoreVoiceState value)
		{
			return value.UserId.ToString();
		}
	}
}

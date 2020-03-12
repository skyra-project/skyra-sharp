using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public class VoiceStateStore : HashMapCacheStoreBase<CoreVoiceState>
	{
		public VoiceStateStore(CacheClient client) : base(client, "voiceStates")
		{
		}

		protected override string GetKey(CoreVoiceState value)
		{
			return value.UserId.ToString();
		}
	}
}

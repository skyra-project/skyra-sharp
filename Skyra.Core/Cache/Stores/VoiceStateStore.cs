using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class VoiceStateStore : HashMapCacheStoreBase<VoiceState>
	{
		internal VoiceStateStore(CacheClient context) : base(context, "voiceStates")
		{
		}

		[NotNull]
		protected override string GetKey([NotNull] VoiceState value)
		{
			return value.UserId.ToString();
		}
	}
}

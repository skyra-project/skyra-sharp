using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class CoreUser : ICoreBaseStructure<CoreUser>
	{
		public CoreUser(IClient client, ulong id, bool bot, string username, string discriminator, string? avatar)
		{
			Client = client;
			Id = id;
			Username = username;
			Discriminator = discriminator;
			Avatar = avatar;
			Bot = bot;
		}

		[JsonProperty("id")]
		public ulong Id { get; private set; }

		[JsonProperty("b")]
		public bool Bot { get; private set; }

		[JsonProperty("u")]
		public string Username { get; private set; }

		[JsonProperty("d")]
		public string Discriminator { get; private set; }

		[JsonProperty("a")]
		public string? Avatar { get; private set; }

		[JsonIgnore]
		public IClient Client { get; }

		public CoreUser Patch(CoreUser value)
		{
			Username = value.Username;
			Discriminator = value.Discriminator;
			Avatar = value.Avatar;
			return this;
		}

		public CoreUser Clone()
		{
			return new CoreUser(Client,
				Id,
				Bot,
				Username,
				Discriminator,
				Avatar);
		}

		public static CoreUser From(IClient client, User user)
		{
			return new CoreUser(client, ulong.Parse(user.Id), user.Bot, user.Username, user.Discriminator, user.Avatar);
		}
	}
}

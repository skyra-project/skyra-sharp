using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models
{
	public sealed class User : IBaseStructure<User>
	{
		public User(IClient client, ulong id, bool bot, string username, string discriminator, string? avatar)
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
		public IClient Client { get; set; }

		[NotNull]
		public User Patch([NotNull] User value)
		{
			Username = value.Username;
			Discriminator = value.Discriminator;
			Avatar = value.Avatar;
			return this;
		}

		[NotNull]
		public User Clone()
		{
			return new User(Client,
				Id,
				Bot,
				Username,
				Discriminator,
				Avatar);
		}

		[NotNull]
		public static User From(IClient client, [NotNull] Spectacles.NET.Types.User user)
		{
			return new User(client, ulong.Parse(user.Id), user.Bot, user.Username, user.Discriminator, user.Avatar);
		}
	}
}

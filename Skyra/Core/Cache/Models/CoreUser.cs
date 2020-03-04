using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreUser : ICoreBaseStructure<CoreUser>
	{
		public CoreUser(User user)
		{
			Id = user.Id;
			Bot = user.Bot;
			Username = user.Username;
			Discriminator = user.Discriminator;
			Avatar = user.Avatar;
		}

		[JsonConstructor]
		public CoreUser(string id, bool bot, string username, string discriminator, string? avatar)
		{
			Id = id;
			Username = username;
			Discriminator = discriminator;
			Avatar = avatar;
			Bot = bot;
		}

		[JsonProperty("id")]
		public string Id { get; private set; }

		[JsonProperty("b")]
		public bool Bot { get; private set; }

		[JsonProperty("u")]
		public string Username { get; private set; }

		[JsonProperty("d")]
		public string Discriminator { get; private set; }

		[JsonProperty("a")]
		public string? Avatar { get; private set; }

		public CoreUser Patch(CoreUser value)
		{
			Username = value.Username;
			Discriminator = value.Discriminator;
			Avatar = value.Avatar;
			return this;
		}

		public CoreUser Clone()
		{
			return new CoreUser(Id,
				Bot,
				Username,
				Discriminator,
				Avatar);
		}
	}
}

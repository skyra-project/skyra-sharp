using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreUser : ICoreBaseStructure<CoreUser>
	{
		public CoreUser(User user)
		{
			Id = user.Id;
			Username = user.Username;
			Discriminator = user.Discriminator;
			Avatar = user.Avatar;
		}

		[JsonConstructor]
		public CoreUser(string id, string username, string discriminator, string? avatar)
		{
			Id = id;
			Username = username;
			Discriminator = discriminator;
			Avatar = avatar;
		}

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("u")]
		public string Username { get; set; }

		[JsonProperty("d")]
		public string Discriminator { get; set; }

		[JsonProperty("a")]
		public string? Avatar { get; set; }

		public void Patch(CoreUser value)
		{
			Username = value.Username;
			Discriminator = value.Discriminator;
			Avatar = value.Avatar;
		}

		public CoreUser Clone()
		{
			return new CoreUser(Id,
				Username,
				Discriminator,
				Avatar);
		}
	}
}

using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Cache.Models
{
	public class CachedUser
	{
		public CachedUser(User user)
		{
			Id = user.Id;
			Username = user.Username;
			Discriminator = user.Discriminator;
			Avatar = user.Avatar;
		}

		[JsonConstructor]
		public CachedUser(string id, string username, string discriminator, string? avatar)
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
	}
}

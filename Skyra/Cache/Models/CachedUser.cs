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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Skyra.Notifi.Core.Database.Models.Twitch
{
	public sealed class TwitchSubscription
	{
		[Key] [Column("id")] public ulong Id { get; set; }

		[Required] [Column("twitch_id")] public string TwitchID { get; set; }

		[Column("subscribed_guilds", TypeName = "JSONB")]
		public string GuildsRaw
		{
			get => JsonConvert.SerializeObject(Guilds);
			set => Guilds = JsonConvert.DeserializeObject<TwitchSubscriptionGuild[]>(value);
		}

		[NotMapped] public TwitchSubscriptionGuild[] Guilds { get; set; } = new TwitchSubscriptionGuild[0];
	}
}

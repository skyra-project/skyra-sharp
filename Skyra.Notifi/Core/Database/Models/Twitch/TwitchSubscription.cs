using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Notifi.Core.Database.Models.Twitch
{
	public sealed class TwitchSubscription
	{
		[Key]
		[Column("id")]
		public long Id { get; set; }

		[Required]
		[Column("twitch_id")]
		public string TwitchID { get; set; }
	}
}

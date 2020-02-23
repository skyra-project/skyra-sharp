using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Core.Database.Models
{
	[Table("user_game_integrations")]
	public sealed class UserGameIntegrations
	{
		/// <summary>
		/// 	An array of saved FFXIV characters a user has.
		/// </summary>
		[Column("ffxiv")]
		public UserGameIntegrationsFFXIV[] FFXIV { get; set; }
	}
}

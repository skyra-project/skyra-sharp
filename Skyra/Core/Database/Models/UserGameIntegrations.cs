using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Core.Database.Models
{
	[Table("user_game_integrations")]
	public sealed class UserGameIntegrations
	{
		[Column("ffxiv")]
		public UserGameIntegrationsFFXIV[] FFXIV { get; set; }
	}
}

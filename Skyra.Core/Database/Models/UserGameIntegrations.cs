using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Skyra.Core.Database.Models
{
	/// <summary>
	///     The table containing all game integration savable data.
	/// </summary>
	[Table("user_game_integrations")]
	public sealed class UserGameIntegrations
	{
		/// <summary>
		///     The raw value from and for the database. Use <see cref="UserGameIntegrationsFFXIV" />
		/// </summary>
		[Column("ffxiv_characters", TypeName = "JSONB")]
		[NotNull]
		public string FFXIVCharactersRaw
		{
			get => JsonConvert.SerializeObject(FFXIVCharacters);
			set => FFXIVCharacters = JsonConvert.DeserializeObject<UserGameIntegrationsFFXIV[]>(value);
		}

		/// <summary>
		///     An array of saved FFXIV characters a user has.
		/// </summary>
		[NotMapped]
		public UserGameIntegrationsFFXIV[] FFXIVCharacters { get; set; } = new UserGameIntegrationsFFXIV[0];

		/// <summary>
		///     The <see cref="User" /> foreign key and primary key for this entity.
		/// </summary>
		[Key]
		[Column("user_id")]
		public ulong UserId { get; set; }

		/// <summary>
		///     The navigation property to the <see cref="User" /> entity.
		/// </summary>
		public User User { get; set; } = null!;
	}
}

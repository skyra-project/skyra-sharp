using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_social")]
	public sealed class GuildSocial
	{
		/// <summary>
		///     Whether or not this sub-system is enabled in this guild.
		/// </summary>
		[Column("enabled")]
		public bool Enabled { get; set; } = true;

		/// <summary>
		///     The collection of <see cref="Spectacles.NET.Types.Channel" /> IDs that should be ignored from this sub-module.
		/// </summary>
		[Column("ignored_channels")]
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];

		/// <summary>
		///     Whether or not to send level-up messages when a user earns a new level from <see cref="GuildRole.LevelRoles" />.
		/// </summary>
		[Column("level_up_channel")]
		public ulong? LevelUpChannel { get; set; } = null;

		/// <summary>
		///     The multiplier for member points in this guild.
		/// </summary>
		[Column("multiplier")]
		[CustomValidation(typeof(GuildSocial), "CheckMultiplier")]
		public float Multiplier { get; set; } = 1.0f;

		/// <summary>
		///     The <see cref="Guild" /> foreign key and primary key for this entity.
		/// </summary>
		[Key]
		[Column("guild_id")]
		public ulong GuildId { get; set; }

		/// <summary>
		///     The navigation property to the <see cref="Guild" /> entity.
		/// </summary>
		public Guild Guild { get; set; } = null!;

		internal static ValidationResult CheckMultiplier(float value, ValidationContext ctx)
		{
			return value <= 0.0f || value > 5.0f
				? new ValidationResult("You cannot set a multiplier as negative, null, or higher than 5.")
				: ValidationResult.Success;
		}
	}
}

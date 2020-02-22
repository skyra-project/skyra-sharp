using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Core.Database.Models
{
	[Table("clients")]
	public sealed class Client
	{
		/// <summary>
		///     The ID of the bot's User <see cref="Spectacles.NET.Types.User.Id" />.
		/// </summary>
		[Column("id")]
		public ulong Id { get; set; }

		/// <summary>
		///     The amount of command uses registered globally.
		/// </summary>
		[Column("command_uses")]
		public uint CommandUses { get; set; }

		/// <summary>
		///     The user blacklist, keyed by their Discord User <see cref="Spectacles.NET.Types.User.Id" />.
		/// </summary>
		[Column("user_blacklist")]
		public ulong[] UserBlacklist { get; set; } = new ulong[0];

		/// <summary>
		///     The guild blacklist, keyed by their Discord Guild <see cref="Spectacles.NET.Types.Guild.Id" />.
		/// </summary>
		[Column("guild_blacklist")]
		public ulong[] GuildBlacklist { get; set; } = new ulong[0];

		/// <summary>
		///     The boosted users, keyed by their Discord User <see cref="Spectacles.NET.Types.User.Id" />.
		/// </summary>
		[Column("boosts_users")]
		public ulong[] BoostsUsers { get; set; } = new ulong[0];

		/// <summary>
		///     The boosted guilds, keyed by their Discord Guild <see cref="Spectacles.NET.Types.Guild.Id" />.
		/// </summary>
		[Column("boosts_guilds")]
		public ulong[] BoostsGuilds { get; set; } = new ulong[0];
	}
}

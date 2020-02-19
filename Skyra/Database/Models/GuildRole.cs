using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	[Table("guild_roles")]
	public sealed class GuildRole
	{
		/// <summary>
		///     The collection of <see cref="Spectacles.NET.Types.Role" />s which have Administrator permission level.
		/// </summary>
		[Column("administrators")]
		public ulong[] Administrators { get; set; } = new ulong[0];

		/// <summary>
		///     The collection of <see cref="Spectacles.NET.Types.Role" />s which have Moderator permission level.
		/// </summary>
		[Column("moderators")]
		public ulong[] Moderators { get; set; } = new ulong[0];

		/// <summary>
		///     The collection of <see cref="Spectacles.NET.Types.Role" />s which have DJ permissions.
		/// </summary>
		[Column("djs")]
		public ulong[] Dj { get; set; } = new ulong[0];

		/// <summary>
		///     The collection of <see cref="Spectacles.NET.Types.Role" />s which are publicly claimable.
		/// </summary>
		[Column("public")]
		public ulong[] Public { get; set; } = new ulong[0];

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Role" /> which is given to members upon joining the
		///     <see cref="Spectacles.NET.Types.Guild" />.
		/// </summary>
		[Column("initial")]
		public ulong? Initial { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Role" /> which is given to users who are subscribed to the
		///     <see cref="Spectacles.NET.Types.Guild" />'s announcements.
		/// </summary>
		[Column("subscriber")]
		public ulong? Subscriber { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Role" /> which is given to users when they are muted.
		/// </summary>
		[Column("muted")]
		public ulong? Muted { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Role" /> which defines a reaction restriction.
		/// </summary>
		[Column("restricted_reaction")]
		public ulong? RestrictedReaction { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Role" /> which defines a embed restriction.
		/// </summary>
		[Column("restricted_embed")]
		public ulong? RestrictedEmbed { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Role" /> which defines an external emoji restriction.
		/// </summary>
		[Column("restricted_emoji")]
		public ulong? RestrictedEmoji { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Role" /> which defines an attachment restriction.
		/// </summary>
		[Column("restricted_attachment")]
		public ulong? RestrictedAttachment { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Role" /> which defines a voice restriction.
		/// </summary>
		[Column("restricted_voice")]
		public ulong? RestrictedVoice { get; set; } = null;

		/// <summary>
		///     Whether or not the <see cref="Initial" /> role should be removed when claiming any of the public roles.
		/// </summary>
		[Column("remove_initial")]
		public bool RemoveInitial { get; set; } = false;

		/// <summary>
		///     The raw value from and for the database. Use <see cref="LevelRoles" />
		/// </summary>
		[Column("level_roles", TypeName = "JSON[]")]
		public string[] LevelRolesRaw
		{
			get => LevelRoles.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => LevelRoles = value.Select(JsonConvert.DeserializeObject<LevelRole>).ToArray();
		}

		/// <summary>
		///     The level roles which are granted automatically when reaching a certain amount of points.
		/// </summary>
		[NotMapped]
		public LevelRole[] LevelRoles { get; set; } = new LevelRole[0];

		/// <summary>
		///     The raw value from and for the database. Use <see cref="MessageReactions" />
		/// </summary>
		[Column("message_reactions", TypeName = "JSON[]")]
		public string[] MessageReactionsRaw
		{
			get => MessageReactions.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => MessageReactions = value.Select(JsonConvert.DeserializeObject<ReactionRole>).ToArray();
		}

		/// <summary>
		///     The message reactions assigned for this guild.
		/// </summary>
		[NotMapped]
		public ReactionRole[] MessageReactions { get; set; } = new ReactionRole[0];

		/// <summary>
		///     The raw value from and for the database. Use <see cref="UniqueRoleSets" />
		/// </summary>
		[Column("unique_role_sets", TypeName = "JSON[]")]
		public string[] UniqueRoleSetsRaw
		{
			get => UniqueRoleSets.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => UniqueRoleSets = value.Select(JsonConvert.DeserializeObject<RoleSet>).ToArray();
		}

		/// <summary>
		///     The unique role sets assigned for this guild.
		/// </summary>
		[NotMapped]
		public RoleSet[] UniqueRoleSets { get; set; } = new RoleSet[0];

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
	}
}

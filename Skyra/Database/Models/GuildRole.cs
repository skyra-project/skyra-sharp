using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	[Table("guild_roles")]
	public sealed class GuildRole
	{
		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Guild" />.
		/// </summary>
		[Column("id")]
		public ulong Id { get; set; }

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
		///     The level roles which are granted automatically when reaching a certain amount of points.
		/// </summary>
		[Column("level_roles", TypeName = "JSON[]")]
		public string[] LevelRolesString
		{
			get => LevelRoles.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => LevelRoles = value.Select(JsonConvert.DeserializeObject<LevelRole>).ToArray();
		}

		[NotMapped]
		public LevelRole[] LevelRoles { get; set; } = new LevelRole[0];

		/// <summary>
		///     The message reactions assigned for this guild.
		/// </summary>
		[Column("message_reactions", TypeName = "JSON[]")]
		public string[] MessageReactionsString
		{
			get => MessageReactions.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => MessageReactions = value.Select(JsonConvert.DeserializeObject<ReactionRole>).ToArray();
		}

		[NotMapped]
		public ReactionRole[] MessageReactions { get; set; } = new ReactionRole[0];

		/// <summary>
		///     The unique role sets assigned for this guild.
		/// </summary>
		[Column("unique_role_sets", TypeName = "JSON[]")]
		public string[] UniqueRoleSetsString
		{
			get => UniqueRoleSets.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => UniqueRoleSets = value.Select(JsonConvert.DeserializeObject<RoleSet>).ToArray();
		}

		[NotMapped]
		public RoleSet[] UniqueRoleSets { get; set; } = new RoleSet[0];

		public ulong GuildId { get; set; }
		public Guild Guild { get; set; }
	}
}

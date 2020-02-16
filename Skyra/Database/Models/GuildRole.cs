namespace Skyra.Database.Models
{
	public class GuildRole
	{
		public string Administrator { get; set; }
		public GuildRolesAutomatic[] Automatic { get; set; }
		public ulong Initial { get; set; }
		public GuildRolesReaction[] MessageReactions { get; set; }
		public ulong Moderator { get; set; }
		public ulong Muted { get; set; }
		public ulong RestrictedReaction { get; set; }
		public ulong RestrictedEmbed { get; set; }
		public ulong RestrictedAttachment { get; set; }
		public ulong RestrictedVoice { get; set; }
		public ulong[] Public { get; set; }
		public bool RemoveInitial { get; set; }
		public ulong Dj { get; set; }
		public ulong Subscriber { get; set; }
		public GuildRolesRoleSet[] UniqueRoleSets { get; set; }

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}

	public struct GuildRolesAutomatic
	{
		public ulong RoleId { get; set; }
		public ulong Points { get; set; }
	}

	public struct GuildRolesReaction
	{
		public ulong RoleId { get; set; }
		public ulong ChannelId { get; set; }
		public ulong MessageId { get; set; }
	}

	public struct GuildRolesRoleSet
	{
		public string Name { get; set; }
		public ulong Roles { get; set; }
	}
}

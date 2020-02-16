namespace Skyra.Database.Models
{
	public sealed class GuildRole
	{
		public ulong Id { get; set; }
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
		public Guild Guild { get; set; }
	}
}

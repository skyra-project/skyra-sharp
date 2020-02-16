using System;
using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	public class Guild
	{
		public ulong Id { get; set; }
		public string Prefix { get; set; } = "s!";
		public bool DisableNaturalPrefix { get; set; } = false;
		public string Language { get; set; } = "en-US";
		public uint CommandUses { get; set; } = 0;
		public string[] DisabledCommands { get; set; } = new string[0];
		public GuildTag[] Tags { get; set; } = new GuildTag[0];
		public GuildStickyRole[] StickyRoles { get; set; } = new GuildStickyRole[0];
		public GuildCommandAutoDelete[] CommandAutoDelete { get; set; } = new GuildCommandAutoDelete[0];

		public GuildDisabledCommandChannels[] DisabledCommandChannels { get; set; } =
			new GuildDisabledCommandChannels[0];

		public GuildPermission Permission { get; set; }
		public GuildChannel Channel { get; set; }
		public GuildEvent Event { get; set; }
		public GuildMessage Message { get; set; }
		public GuildRole Role { get; set; }
		public GuildAutoModeration AutoModeration { get; set; }
		public GuildSocial Social { get; set; }
		public GuildAction Action { get; set; }
		public GuildMusic Music { get; set; }
	}

	public class GuildTag(string name, string content)
	{
	}

	public class GuildCommandAutoDelete(string command, TimeSpan duration)
	{
	}

	public class GuildDisabledCommandChannels
	{
		[JsonProperty("channel")]
		public ulong Channel { get; set; }

		[JsonProperty("commands")]
		public string[] Commands { get; set; } = new string[0];
	}

	public class GuildStickyRole
	{
		[JsonProperty("user")]
		public ulong User { get; set; }

		[JsonProperty("roles")]
		public ulong[] Roles { get; set; } = new ulong[0];
	}

	public class GuildPermissionNode
	{
		[JsonProperty("id")]
		public ulong Id { get; set; }

		[JsonProperty("commands")]
		public string[] Commands { get; set; }
	}

	public class GuildPermission
	{
		public GuildPermissionNode[] Users { get; set; } = new GuildPermissionNode[0];

		public GuildPermissionNode[] Roles { get; set; } = new GuildPermissionNode[0];

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}

	public class GuildChannel
	{
		public ulong? Announcements { get; set; } = null;
		public ulong? Greetings { get; set; } = null;
		public ulong? Farewell { get; set; } = null;
		public ulong? MemberLogs { get; set; } = null;
		public ulong? ModerationLogs { get; set; } = null;
		public ulong? MessageLogs { get; set; } = null;
		public ulong? NsfwMessageLogs { get; set; } = null;
		public ulong? ImageLogs { get; set; } = null;
		public ulong? PruneLogs { get; set; } = null;
		public ulong? ReactionLogs { get; set; } = null;

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}

	public class GuildEvent
	{
		public bool BanAdd { get; set; } = false;
		public bool BanRemove { get; set; } = false;
		public bool MemberAdd { get; set; } = false;
		public bool MemberRemove { get; set; } = false;
		public bool MemberNicknameUpdate { get; set; } = false;
		public bool MessageDelete { get; set; } = false;
		public bool MessageEdit { get; set; } = false;
		public bool Twemoji { get; set; } = false;

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}

	public class GuildMessage
	{
		public string? Farewell { get; set; } = null;
		public string? Greeting { get; set; } = null;
		public string? JoinDm { get; set; } = null;
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];
		public bool ModerationDm { get; set; } = false;
		public bool ModerationReasonDisplay { get; set; } = true;
		public bool ModerationMessageDisplay { get; set; } = true;
		public bool ModerationAutoDelete { get; set; } = false;
		public bool ModeratorNameDisplay { get; set; } = true;

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}

	public class GuildRolesAutomatic
	{
		[JsonProperty("role_id")]
		public ulong RoleId { get; set; }

		[JsonProperty("points")]
		public ulong Points { get; set; }
	}

	public class GuildRolesReaction
	{
		[JsonProperty("role_id")]
		public ulong RoleId { get; set; }

		[JsonProperty("channel_id")]
		public ulong ChannelId { get; set; }

		[JsonProperty("message_id")]
		public ulong MessageId { get; set; }
	}

	public class GuildRolesRoleSet
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("roles")]
		public ulong Roles { get; set; }
	}

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

	public class GuildAutoModeration
	{
		public ulong[] IgnoredRoles { get; set; } = new ulong[0];
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];

		public GuildModerationAttachment Attachment { get; set; }
		public GuildModerationCapital Capital { get; set; }
		public GuildModerationLink Link { get; set; }
		public GuildModerationMessage Message { get; set; }
		public GuildModerationNewLine NewLine { get; set; }
		public GuildModerationInvites Invites { get; set; }
		public GuildModerationWord Word { get; set; }
		public GuildModerationReaction Reaction { get; set; }
		public GuildModerationNoMentionSpam NoMentionSpam { get; set; }

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}

	public enum GuildModerationSoftAction
	{
		None
	}

	public enum GuildModerationHardAction
	{
		None
	}

	public abstract class GuildModerationShared
	{
		public bool Enabled { get; set; } = false;
		public ulong[] IgnoredRoles { get; set; } = new ulong[0];
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];
		public GuildModerationSoftAction SoftActionType { get; set; } = GuildModerationSoftAction.None;
		public ushort SoftActionThresholdMaximum { get; set; } = 120;
		public TimeSpan SoftActionThresholdDuration { get; set; } = TimeSpan.FromMinutes(2);
		public GuildModerationHardAction HardActionType { get; set; } = GuildModerationHardAction.None;
		public TimeSpan HardActionDuration { get; set; } = TimeSpan.Zero;
		public ushort HardActionThresholdMaximum { get; set; } = 120;
		public TimeSpan HardActionThresholdDuration { get; set; } = TimeSpan.FromMinutes(2);
	}

	public class GuildModerationAttachment : GuildModerationShared
	{
		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationCapital : GuildModerationShared
	{
		public ushort MinimumCharacters { get; set; } = 10;
		public ushort PercentageCharacters { get; set; } = 50;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationLink : GuildModerationShared
	{
		public string[] WhiteList { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationMessage : GuildModerationShared
	{
		public ushort QueueSize { get; set; } = 50;
		public ushort MaximumDuplicates { get; set; } = 5;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationNewLine : GuildModerationShared
	{
		public ushort Maximum { get; set; } = 5;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationInvites : GuildModerationShared
	{
		public ulong[] GuildWhiteList { get; set; } = new ulong[0];
		public string[] CodeWhiteList { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationWord : GuildModerationShared
	{
		public string[] Words { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationReaction : GuildModerationShared
	{
		public ushort Maximum { get; set; } = 10;
		public string[] WhiteList { get; set; } = new string[0];
		public string[] BlackList { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationNoMentionSpam : GuildModerationShared
	{
		public ushort Maximum { get; set; } = 10;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildSocial
	{
		public bool Enabled { get; set; } = true;
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];
		public ulong? LevelUpChannel { get; set; } = null;
		public float Multiplier { get; set; } = 1.0f;

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}

	public class GuildActionAlias
	{
		[JsonProperty("command")]
		public string Command { get; set; }

		[JsonProperty("alias")]
		public string Alias { get; set; }
	}

	public enum GuildActionTriggerTypes
	{
		React
	}

	public class GuildActionTrigger
	{
		[JsonProperty("type")]
		public GuildActionTriggerTypes Type { get; set; } = GuildActionTriggerTypes.React;

		[JsonProperty("input")]
		public string Input { get; set; }

		[JsonProperty("output")]
		public string Output { get; set; }
	}

	public class GuildAction
	{
		public GuildActionAlias Alias { get; set; }
		public GuildActionTrigger Trigger { get; set; }

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}

	public class GuildMusic
	{
		public uint DefaultVolume { get; set; } = 100;
		public TimeSpan MaximumDuration { get; set; } = TimeSpan.FromHours(1);
		public ushort MaximumEntriesPerUser { get; set; } = 100;
		public bool AllowStreams { get; set; } = false;

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}
}

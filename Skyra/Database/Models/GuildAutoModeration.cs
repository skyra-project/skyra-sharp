using System;

namespace Skyra.Database.Models
{
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

	public abstract class GuildModerationBase
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

	public class GuildModerationAttachment : GuildModerationBase
	{
		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationCapital : GuildModerationBase
	{
		public ushort MinimumCharacters { get; set; } = 10;
		public ushort PercentageCharacters { get; set; } = 50;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationLink : GuildModerationBase
	{
		public string[] WhiteList { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationMessage : GuildModerationBase
	{
		public ushort QueueSize { get; set; } = 50;
		public ushort MaximumDuplicates { get; set; } = 5;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationNewLine : GuildModerationBase
	{
		public ushort Maximum { get; set; } = 5;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationInvites : GuildModerationBase
	{
		public ulong[] GuildWhiteList { get; set; } = new ulong[0];
		public string[] CodeWhiteList { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationWord : GuildModerationBase
	{
		public string[] Words { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationReaction : GuildModerationBase
	{
		public ushort Maximum { get; set; } = 10;
		public string[] WhiteList { get; set; } = new string[0];
		public string[] BlackList { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}

	public class GuildModerationNoMentionSpam : GuildModerationBase
	{
		public ushort Maximum { get; set; } = 10;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}
}

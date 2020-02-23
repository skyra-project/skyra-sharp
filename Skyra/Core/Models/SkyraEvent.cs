namespace Skyra.Core.Models
{
	public enum SkyraEvent
	{
		/// <summary>
		///     Contains the initial state information.
		/// </summary>
		READY,

		/// <summary>
		///     Response to Resume.
		/// </summary>
		RESUMED,

		/// <summary>
		///     New channel created.
		/// </summary>
		CHANNEL_CREATE,

		/// <summary>
		///     Channel was updated.
		/// </summary>
		CHANNEL_UPDATE,

		/// <summary>
		///     Channel was deleted.
		/// </summary>
		CHANNEL_DELETE,

		/// <summary>
		///     Message was pinned or unpinned.
		/// </summary>
		CHANNEL_PINS_UPDATE,

		/// <summary>
		///     Lazy-load for unavailable guild, guild became available, or user joined a new guild.
		/// </summary>
		GUILD_CREATE,

		/// <summary>
		///     Guild was updated.
		/// </summary>
		GUILD_UPDATE,

		/// <summary>
		///     Guild became unavailable, or user left/was removed from a guild.
		/// </summary>
		GUILD_DELETE,

		/// <summary>
		///     User was banned from a guild.
		/// </summary>
		GUILD_BAN_ADD,

		/// <summary>
		///     User was unbanned from a guild.
		/// </summary>
		GUILD_BAN_REMOVE,

		/// <summary>
		///     Guild emojis were updated.
		/// </summary>
		GUILD_EMOJIS_UPDATE,

		/// <summary>
		///     Guild integration was updated.
		/// </summary>
		GUILD_INTEGRATIONS_UPDATE,

		/// <summary>
		///     New user joined a guild.
		/// </summary>
		GUILD_MEMBER_ADD,

		/// <summary>
		///     User was removed from a guild.
		/// </summary>
		GUILD_MEMBER_REMOVE,

		/// <summary>
		///     Guild member was updated.
		/// </summary>
		GUILD_MEMBER_UPDATE,

		/// <summary>
		///     Response to Request Guild Members.
		/// </summary>
		GUILD_MEMBERS_CHUNK,

		/// <summary>
		///     Guild role was created.
		/// </summary>
		GUILD_ROLE_CREATE,

		/// <summary>
		///     Guild role was updated.
		/// </summary>
		GUILD_ROLE_UPDATE,

		/// <summary>
		///     Guild role was deleted.
		/// </summary>
		GUILD_ROLE_DELETE,

		/// <summary>
		/// 	Invite to a channel was created.
		/// </summary>
		INVITE_CREATE,

		/// <summary>
		/// 	Invite to a channel was deleted.
		/// </summary>
		INVITE_DELETE,

		/// <summary>
		///     Message was created.
		/// </summary>
		MESSAGE_CREATE,

		/// <summary>
		///     Message was edited.
		/// </summary>
		MESSAGE_UPDATE,

		/// <summary>
		///     Message was deleted.
		/// </summary>
		MESSAGE_DELETE,

		/// <summary>
		///     Multiple messages were deleted at once.
		/// </summary>
		MESSAGE_DELETE_BULK,

		/// <summary>
		///     User reacted to a message.
		/// </summary>
		MESSAGE_REACTION_ADD,

		/// <summary>
		///     User removed a reaction from a message.
		/// </summary>
		MESSAGE_REACTION_REMOVE,

		/// <summary>
		///     All reactions were explicitly removed from a message.
		/// </summary>
		MESSAGE_REACTION_REMOVE_ALL,

		/// <summary>
		/// 	All reactions for a given emoji were explicitly removed from a message.
		/// </summary>
		MESSAGE_REACTION_REMOVE_EMOJI,

		/// <summary>
		///     User was updated.
		/// </summary>
		PRESENCE_UPDATE,

		/// <summary>
		///     Used to replace presences after outages, data is null for bot accounts receiving it.
		///     <remarks>Bot Accounts should ignore this.</remarks>
		/// </summary>
		PRESENCES_REPLACE,

		/// <summary>
		///     User started typing in a channel.
		/// </summary>
		TYPING_START,

		/// <summary>
		///     Properties about the user changed.
		/// </summary>
		USER_UPDATE,

		/// <summary>
		///     Someone joined, left, or moved a voice channel.
		/// </summary>
		VOICE_STATE_UPDATE,

		/// <summary>
		///     Guild's voice server was updated.
		/// </summary>
		VOICE_SERVER_UPDATE,

		/// <summary>
		///     Guild channel webhook was created, update, or deleted.
		/// </summary>
		WEBHOOKS_UPDATE,

		/// <summary>
		/// 	A twitch stream has started.
		/// </summary>

		NOTIFY_TWITCH_STREAM_START,

		/// <summary>
		/// 	A twitch stream has ended.
		/// </summary>

		NOTIFY_TWITCH_STREAM_END
	}
}

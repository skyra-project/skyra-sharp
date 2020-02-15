using System;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Skyra.Database.Models;

namespace Skyra.Database
{
	public class SkyraDatabaseContext : DbContext
	{
		public SkyraDatabaseContext()
		{
		}

		public SkyraDatabaseContext(DbContextOptions options)
			: base(options)
		{
		}

		public virtual DbSet<Banners> Banners { get; set; }
		public virtual DbSet<ClientStorage> ClientStorage { get; set; }
		public virtual DbSet<CommandCounter> CommandCounter { get; set; }
		public virtual DbSet<DashboardUsers> DashboardUsers { get; set; }
		public virtual DbSet<Giveaway> Giveaway { get; set; }
		public virtual DbSet<Guilds> Guilds { get; set; }
		public virtual DbSet<Members> Members { get; set; }
		public virtual DbSet<Moderation> Moderation { get; set; }
		public virtual DbSet<Starboard> Starboard { get; set; }
		public virtual DbSet<TwitchStreamSubscriptions> TwitchStreamSubscriptions { get; set; }
		public virtual DbSet<Users> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured) return;

			var user = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
			var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "";
			var host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
			var port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
			var name = Environment.GetEnvironmentVariable("POSTGRES_NAME") ?? "skyra";

			optionsBuilder.UseNpgsql(
				$"User ID={user};Password={password};Server={host};Port={port};Database={name};Pooling=true;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Banners>(entity =>
			{
				entity.ToTable("banners");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasMaxLength(6);

				entity.Property(e => e.AuthorId)
					.IsRequired()
					.HasColumnName("author_id")
					.HasMaxLength(19);

				entity.Property(e => e.Group)
					.IsRequired()
					.HasColumnName("group")
					.HasMaxLength(32);

				entity.Property(e => e.Price).HasColumnName("price");

				entity.Property(e => e.Title)
					.IsRequired()
					.HasColumnName("title")
					.HasMaxLength(128);
			});

			modelBuilder.Entity<ClientStorage>(entity =>
			{
				entity.ToTable("clientStorage");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasMaxLength(19);

				entity.Property(e => e.BoostsGuilds)
					.IsRequired()
					.HasColumnName("boosts_guilds")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.BoostsUsers)
					.IsRequired()
					.HasColumnName("boosts_users")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.CommandUses).HasColumnName("commandUses");

				entity.Property(e => e.GuildBlacklist)
					.IsRequired()
					.HasColumnName("guildBlacklist")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.Schedules)
					.IsRequired()
					.HasColumnName("schedules")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.UserBlacklist)
					.IsRequired()
					.HasColumnName("userBlacklist")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");
			});

			modelBuilder.Entity<CommandCounter>(entity =>
			{
				entity.ToTable("command_counter");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasMaxLength(32);

				entity.Property(e => e.Uses).HasColumnName("uses");
			});

			modelBuilder.Entity<DashboardUsers>(entity =>
			{
				entity.ToTable("dashboard_users");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasMaxLength(19);

				entity.Property(e => e.AccessToken)
					.IsRequired()
					.HasColumnName("access_token")
					.HasMaxLength(30)
					.IsFixedLength();

				entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");

				entity.Property(e => e.RefreshToken)
					.IsRequired()
					.HasColumnName("refresh_token")
					.HasMaxLength(30)
					.IsFixedLength();
			});

			modelBuilder.Entity<Giveaway>(entity =>
			{
				entity.HasKey(e => new {e.GuildId, e.MessageId})
					.HasName("giveaway_guild_message_idx");

				entity.ToTable("giveaway");

				entity.Property(e => e.GuildId)
					.HasColumnName("guild_id")
					.HasMaxLength(19);

				entity.Property(e => e.MessageId)
					.HasColumnName("message_id")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelId)
					.IsRequired()
					.HasColumnName("channel_id")
					.HasMaxLength(19);

				entity.Property(e => e.EndsAt).HasColumnName("ends_at");

				entity.Property(e => e.Minimum)
					.HasColumnName("minimum")
					.HasDefaultValueSql("1");

				entity.Property(e => e.MinimumWinners)
					.HasColumnName("minimum_winners")
					.HasDefaultValueSql("1");

				entity.Property(e => e.Title)
					.IsRequired()
					.HasColumnName("title")
					.HasMaxLength(256);
			});

			modelBuilder.Entity<Guilds>(entity =>
			{
				entity.ToTable("guilds");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsAnnouncements)
					.HasColumnName("channels.announcements")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsFarewell)
					.HasColumnName("channels.farewell")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsGreeting)
					.HasColumnName("channels.greeting")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsImageLogs)
					.HasColumnName("channels.image-logs")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsMemberLogs)
					.HasColumnName("channels.member-logs")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsMessageLogs)
					.HasColumnName("channels.message-logs")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsModerationLogs)
					.HasColumnName("channels.moderation-logs")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsNsfwMessageLogs)
					.HasColumnName("channels.nsfw-message-logs")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsPruneLogs)
					.HasColumnName("channels.prune-logs")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsReactionLogs)
					.HasColumnName("channels.reaction-logs")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsRoles)
					.HasColumnName("channels.roles")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelsSpam)
					.HasColumnName("channels.spam")
					.HasMaxLength(19);

				entity.Property(e => e.CommandAutodelete)
					.IsRequired()
					.HasColumnName("command-autodelete")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.CommandUses).HasColumnName("commandUses");

				entity.Property(e => e.DisableNaturalPrefix).HasColumnName("disableNaturalPrefix");

				entity.Property(e => e.DisabledChannels)
					.IsRequired()
					.HasColumnName("disabledChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.DisabledCommands)
					.IsRequired()
					.HasColumnName("disabledCommands")
					.HasColumnType("character varying(32)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.DisabledCommandsChannels)
					.IsRequired()
					.HasColumnName("disabledCommandsChannels")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.EventsBanAdd).HasColumnName("events.banAdd");

				entity.Property(e => e.EventsBanRemove).HasColumnName("events.banRemove");

				entity.Property(e => e.EventsMemberAdd).HasColumnName("events.memberAdd");

				entity.Property(e => e.EventsMemberNameUpdate).HasColumnName("events.memberNameUpdate");

				entity.Property(e => e.EventsMemberRemove).HasColumnName("events.memberRemove");

				entity.Property(e => e.EventsMessageDelete).HasColumnName("events.messageDelete");

				entity.Property(e => e.EventsMessageEdit).HasColumnName("events.messageEdit");

				entity.Property(e => e.EventsTwemojiReactions).HasColumnName("events.twemoji-reactions");

				entity.Property(e => e.Language)
					.IsRequired()
					.HasColumnName("language")
					.HasMaxLength(5)
					.HasDefaultValueSql("'en-US'::character varying");

				entity.Property(e => e.MessagesFarewell)
					.HasColumnName("messages.farewell")
					.HasMaxLength(2000);

				entity.Property(e => e.MessagesGreeting)
					.HasColumnName("messages.greeting")
					.HasMaxLength(2000);

				entity.Property(e => e.MessagesIgnoreChannels)
					.IsRequired()
					.HasColumnName("messages.ignoreChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.MessagesJoinDm)
					.HasColumnName("messages.join-dm")
					.HasMaxLength(1500);

				entity.Property(e => e.MessagesModerationAutoDelete).HasColumnName("messages.moderation-auto-delete");

				entity.Property(e => e.MessagesModerationDm).HasColumnName("messages.moderation-dm");

				entity.Property(e => e.MessagesModerationMessageDisplay)
					.IsRequired()
					.HasColumnName("messages.moderation-message-display")
					.HasDefaultValueSql("true");

				entity.Property(e => e.MessagesModerationReasonDisplay)
					.IsRequired()
					.HasColumnName("messages.moderation-reason-display")
					.HasDefaultValueSql("true");

				entity.Property(e => e.MessagesModeratorNameDisplay)
					.IsRequired()
					.HasColumnName("messages.moderator-name-display")
					.HasDefaultValueSql("true");

				entity.Property(e => e.MusicAllowStreams)
					.IsRequired()
					.HasColumnName("music.allow-streams")
					.HasDefaultValueSql("true");

				entity.Property(e => e.MusicDefaultVolume)
					.HasColumnName("music.default-volume")
					.HasDefaultValueSql("100");

				entity.Property(e => e.MusicMaximumDuration)
					.HasColumnName("music.maximum-duration")
					.HasDefaultValueSql("7200000");

				entity.Property(e => e.MusicMaximumEntriesPerUser)
					.HasColumnName("music.maximum-entries-per-user")
					.HasDefaultValueSql("100");

				entity.Property(e => e.NoMentionSpamAlerts).HasColumnName("no-mention-spam.alerts");

				entity.Property(e => e.NoMentionSpamEnabled).HasColumnName("no-mention-spam.enabled");

				entity.Property(e => e.NoMentionSpamMentionsAllowed)
					.HasColumnName("no-mention-spam.mentionsAllowed")
					.HasDefaultValueSql("20");

				entity.Property(e => e.NoMentionSpamTimePeriod)
					.HasColumnName("no-mention-spam.timePeriod")
					.HasDefaultValueSql("8");

				entity.Property(e => e.NotificationsStreamsTwitchStreamers)
					.IsRequired()
					.HasColumnName("notifications.streams.twitch.streamers")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.PermissionsRoles)
					.IsRequired()
					.HasColumnName("permissions.roles")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.PermissionsUsers)
					.IsRequired()
					.HasColumnName("permissions.users")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.Prefix)
					.IsRequired()
					.HasColumnName("prefix")
					.HasMaxLength(10)
					.HasDefaultValueSql("'s!'::character varying");

				entity.Property(e => e.RolesAdmin)
					.HasColumnName("roles.admin")
					.HasMaxLength(19);

				entity.Property(e => e.RolesAuto)
					.IsRequired()
					.HasColumnName("roles.auto")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.RolesDj)
					.HasColumnName("roles.dj")
					.HasMaxLength(19);

				entity.Property(e => e.RolesInitial)
					.HasColumnName("roles.initial")
					.HasMaxLength(19);

				entity.Property(e => e.RolesMessageReaction)
					.HasColumnName("roles.messageReaction")
					.HasMaxLength(19);

				entity.Property(e => e.RolesModerator)
					.HasColumnName("roles.moderator")
					.HasMaxLength(19);

				entity.Property(e => e.RolesMuted)
					.HasColumnName("roles.muted")
					.HasMaxLength(19);

				entity.Property(e => e.RolesPublic)
					.IsRequired()
					.HasColumnName("roles.public")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.RolesReactions)
					.IsRequired()
					.HasColumnName("roles.reactions")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.RolesRemoveInitial).HasColumnName("roles.removeInitial");

				entity.Property(e => e.RolesRestrictedAttachment)
					.HasColumnName("roles.restricted-attachment")
					.HasMaxLength(19);

				entity.Property(e => e.RolesRestrictedEmbed)
					.HasColumnName("roles.restricted-embed")
					.HasMaxLength(19);

				entity.Property(e => e.RolesRestrictedReaction)
					.HasColumnName("roles.restricted-reaction")
					.HasMaxLength(19);

				entity.Property(e => e.RolesRestrictedVoice)
					.HasColumnName("roles.restricted-voice")
					.HasMaxLength(19);

				entity.Property(e => e.RolesStaff)
					.HasColumnName("roles.staff")
					.HasMaxLength(19);

				entity.Property(e => e.RolesSubscriber)
					.HasColumnName("roles.subscriber")
					.HasMaxLength(19);

				entity.Property(e => e.RolesUniqueRoleSets)
					.IsRequired()
					.HasColumnName("roles.uniqueRoleSets")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.SelfmodAttachment).HasColumnName("selfmod.attachment");

				entity.Property(e => e.SelfmodAttachmentAction).HasColumnName("selfmod.attachmentAction");

				entity.Property(e => e.SelfmodAttachmentDuration)
					.HasColumnName("selfmod.attachmentDuration")
					.HasDefaultValueSql("20000");

				entity.Property(e => e.SelfmodAttachmentMaximum)
					.HasColumnName("selfmod.attachmentMaximum")
					.HasDefaultValueSql("20");

				entity.Property(e => e.SelfmodAttachmentPunishmentDuration)
					.HasColumnName("selfmod.attachmentPunishmentDuration");

				entity.Property(e => e.SelfmodCapitalsEnabled).HasColumnName("selfmod.capitals.enabled");

				entity.Property(e => e.SelfmodCapitalsHardAction).HasColumnName("selfmod.capitals.hardAction");

				entity.Property(e => e.SelfmodCapitalsHardActionDuration)
					.HasColumnName("selfmod.capitals.hardActionDuration");

				entity.Property(e => e.SelfmodCapitalsIgnoredChannels)
					.IsRequired()
					.HasColumnName("selfmod.capitals.ignoredChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodCapitalsIgnoredRoles)
					.IsRequired()
					.HasColumnName("selfmod.capitals.ignoredRoles")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodCapitalsMaximum)
					.HasColumnName("selfmod.capitals.maximum")
					.HasDefaultValueSql("50");

				entity.Property(e => e.SelfmodCapitalsMinimum)
					.HasColumnName("selfmod.capitals.minimum")
					.HasDefaultValueSql("15");

				entity.Property(e => e.SelfmodCapitalsSoftAction).HasColumnName("selfmod.capitals.softAction");

				entity.Property(e => e.SelfmodCapitalsThresholdDuration)
					.HasColumnName("selfmod.capitals.thresholdDuration")
					.HasDefaultValueSql("60000");

				entity.Property(e => e.SelfmodCapitalsThresholdMaximum)
					.HasColumnName("selfmod.capitals.thresholdMaximum")
					.HasDefaultValueSql("10");

				entity.Property(e => e.SelfmodFilterEnabled).HasColumnName("selfmod.filter.enabled");

				entity.Property(e => e.SelfmodFilterHardAction).HasColumnName("selfmod.filter.hardAction");

				entity.Property(e => e.SelfmodFilterHardActionDuration)
					.HasColumnName("selfmod.filter.hardActionDuration");

				entity.Property(e => e.SelfmodFilterIgnoredChannels)
					.IsRequired()
					.HasColumnName("selfmod.filter.ignoredChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodFilterIgnoredRoles)
					.IsRequired()
					.HasColumnName("selfmod.filter.ignoredRoles")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodFilterRaw)
					.IsRequired()
					.HasColumnName("selfmod.filter.raw")
					.HasColumnType("character varying(32)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.SelfmodFilterSoftAction).HasColumnName("selfmod.filter.softAction");

				entity.Property(e => e.SelfmodFilterThresholdDuration)
					.HasColumnName("selfmod.filter.thresholdDuration")
					.HasDefaultValueSql("60000");

				entity.Property(e => e.SelfmodFilterThresholdMaximum)
					.HasColumnName("selfmod.filter.thresholdMaximum")
					.HasDefaultValueSql("10");

				entity.Property(e => e.SelfmodIgnoreChannels)
					.IsRequired()
					.HasColumnName("selfmod.ignoreChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.SelfmodInvitesEnabled).HasColumnName("selfmod.invites.enabled");

				entity.Property(e => e.SelfmodInvitesHardAction).HasColumnName("selfmod.invites.hardAction");

				entity.Property(e => e.SelfmodInvitesHardActionDuration)
					.HasColumnName("selfmod.invites.hardActionDuration");

				entity.Property(e => e.SelfmodInvitesIgnoredChannels)
					.IsRequired()
					.HasColumnName("selfmod.invites.ignoredChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodInvitesIgnoredRoles)
					.IsRequired()
					.HasColumnName("selfmod.invites.ignoredRoles")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodInvitesSoftAction).HasColumnName("selfmod.invites.softAction");

				entity.Property(e => e.SelfmodInvitesThresholdDuration)
					.HasColumnName("selfmod.invites.thresholdDuration")
					.HasDefaultValueSql("60000");

				entity.Property(e => e.SelfmodInvitesThresholdMaximum)
					.HasColumnName("selfmod.invites.thresholdMaximum")
					.HasDefaultValueSql("10");

				entity.Property(e => e.SelfmodLinksEnabled).HasColumnName("selfmod.links.enabled");

				entity.Property(e => e.SelfmodLinksHardAction).HasColumnName("selfmod.links.hardAction");

				entity.Property(e => e.SelfmodLinksHardActionDuration)
					.HasColumnName("selfmod.links.hardActionDuration");

				entity.Property(e => e.SelfmodLinksIgnoredChannels)
					.IsRequired()
					.HasColumnName("selfmod.links.ignoredChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodLinksIgnoredRoles)
					.IsRequired()
					.HasColumnName("selfmod.links.ignoredRoles")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodLinksSoftAction).HasColumnName("selfmod.links.softAction");

				entity.Property(e => e.SelfmodLinksThresholdDuration)
					.HasColumnName("selfmod.links.thresholdDuration")
					.HasDefaultValueSql("60000");

				entity.Property(e => e.SelfmodLinksThresholdMaximum)
					.HasColumnName("selfmod.links.thresholdMaximum")
					.HasDefaultValueSql("10");

				entity.Property(e => e.SelfmodLinksWhitelist)
					.IsRequired()
					.HasColumnName("selfmod.links.whitelist")
					.HasColumnType("character varying(128)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.SelfmodMessagesEnabled).HasColumnName("selfmod.messages.enabled");

				entity.Property(e => e.SelfmodMessagesHardAction).HasColumnName("selfmod.messages.hardAction");

				entity.Property(e => e.SelfmodMessagesHardActionDuration)
					.HasColumnName("selfmod.messages.hardActionDuration");

				entity.Property(e => e.SelfmodMessagesIgnoredChannels)
					.IsRequired()
					.HasColumnName("selfmod.messages.ignoredChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodMessagesIgnoredRoles)
					.IsRequired()
					.HasColumnName("selfmod.messages.ignoredRoles")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodMessagesMaximum)
					.HasColumnName("selfmod.messages.maximum")
					.HasDefaultValueSql("5");

				entity.Property(e => e.SelfmodMessagesQueueSize)
					.HasColumnName("selfmod.messages.queue-size")
					.HasDefaultValueSql("50");

				entity.Property(e => e.SelfmodMessagesSoftAction).HasColumnName("selfmod.messages.softAction");

				entity.Property(e => e.SelfmodMessagesThresholdDuration)
					.HasColumnName("selfmod.messages.thresholdDuration")
					.HasDefaultValueSql("60000");

				entity.Property(e => e.SelfmodMessagesThresholdMaximum)
					.HasColumnName("selfmod.messages.thresholdMaximum")
					.HasDefaultValueSql("10");

				entity.Property(e => e.SelfmodNewlinesEnabled).HasColumnName("selfmod.newlines.enabled");

				entity.Property(e => e.SelfmodNewlinesHardAction).HasColumnName("selfmod.newlines.hardAction");

				entity.Property(e => e.SelfmodNewlinesHardActionDuration)
					.HasColumnName("selfmod.newlines.hardActionDuration");

				entity.Property(e => e.SelfmodNewlinesIgnoredChannels)
					.IsRequired()
					.HasColumnName("selfmod.newlines.ignoredChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodNewlinesIgnoredRoles)
					.IsRequired()
					.HasColumnName("selfmod.newlines.ignoredRoles")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodNewlinesMaximum)
					.HasColumnName("selfmod.newlines.maximum")
					.HasDefaultValueSql("10");

				entity.Property(e => e.SelfmodNewlinesSoftAction).HasColumnName("selfmod.newlines.softAction");

				entity.Property(e => e.SelfmodNewlinesThresholdDuration)
					.HasColumnName("selfmod.newlines.thresholdDuration")
					.HasDefaultValueSql("60000");

				entity.Property(e => e.SelfmodNewlinesThresholdMaximum)
					.HasColumnName("selfmod.newlines.thresholdMaximum")
					.HasDefaultValueSql("10");

				entity.Property(e => e.SelfmodRaid).HasColumnName("selfmod.raid");

				entity.Property(e => e.SelfmodRaidthreshold)
					.HasColumnName("selfmod.raidthreshold")
					.HasDefaultValueSql("10");

				entity.Property(e => e.SelfmodReactionsBlacklist)
					.IsRequired()
					.HasColumnName("selfmod.reactions.blacklist")
					.HasColumnType("character varying(128)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.SelfmodReactionsEnabled).HasColumnName("selfmod.reactions.enabled");

				entity.Property(e => e.SelfmodReactionsHardAction).HasColumnName("selfmod.reactions.hardAction");

				entity.Property(e => e.SelfmodReactionsHardActionDuration)
					.HasColumnName("selfmod.reactions.hardActionDuration");

				entity.Property(e => e.SelfmodReactionsIgnoredChannels)
					.IsRequired()
					.HasColumnName("selfmod.reactions.ignoredChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodReactionsIgnoredRoles)
					.IsRequired()
					.HasColumnName("selfmod.reactions.ignoredRoles")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying[]");

				entity.Property(e => e.SelfmodReactionsMaximum)
					.HasColumnName("selfmod.reactions.maximum")
					.HasDefaultValueSql("10");

				entity.Property(e => e.SelfmodReactionsSoftAction).HasColumnName("selfmod.reactions.softAction");

				entity.Property(e => e.SelfmodReactionsThresholdDuration)
					.HasColumnName("selfmod.reactions.thresholdDuration")
					.HasDefaultValueSql("60000");

				entity.Property(e => e.SelfmodReactionsThresholdMaximum)
					.HasColumnName("selfmod.reactions.thresholdMaximum")
					.HasDefaultValueSql("10");

				entity.Property(e => e.SelfmodReactionsWhitelist)
					.IsRequired()
					.HasColumnName("selfmod.reactions.whitelist")
					.HasColumnType("character varying(128)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.SocialAchieve).HasColumnName("social.achieve");

				entity.Property(e => e.SocialAchieveMessage)
					.HasColumnName("social.achieveMessage")
					.HasMaxLength(2000);

				entity.Property(e => e.SocialEnabled)
					.IsRequired()
					.HasColumnName("social.enabled")
					.HasDefaultValueSql("true");

				entity.Property(e => e.SocialIgnoreChannels)
					.IsRequired()
					.HasColumnName("social.ignoreChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.SocialMultiplier)
					.HasColumnName("social.multiplier")
					.HasDefaultValueSql("1");

				entity.Property(e => e.StarboardChannel)
					.HasColumnName("starboard.channel")
					.HasMaxLength(19);

				entity.Property(e => e.StarboardEmoji)
					.IsRequired()
					.HasColumnName("starboard.emoji")
					.HasMaxLength(75)
					.HasDefaultValueSql("'%E2%AD%90'::character varying");

				entity.Property(e => e.StarboardIgnoreChannels)
					.IsRequired()
					.HasColumnName("starboard.ignoreChannels")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.StarboardMinimum)
					.HasColumnName("starboard.minimum")
					.HasDefaultValueSql("1");

				entity.Property(e => e.StickyRoles)
					.IsRequired()
					.HasColumnName("stickyRoles")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.Tags)
					.IsRequired()
					.HasColumnName("tags")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.TriggerAlias)
					.IsRequired()
					.HasColumnName("trigger.alias")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");

				entity.Property(e => e.TriggerIncludes)
					.IsRequired()
					.HasColumnName("trigger.includes")
					.HasColumnType("json[]")
					.HasDefaultValueSql("ARRAY[]::json[]");
			});

			modelBuilder.Entity<Members>(entity =>
			{
				entity.HasKey(e => new {e.GuildId, e.UserId})
					.HasName("members_guild_user_idx");

				entity.ToTable("members");

				entity.HasIndex(e => e.PointCount)
					.HasName("members_guild_point_idx")
					.HasSortOrder(SortOrder.Descending);

				entity.Property(e => e.GuildId)
					.HasColumnName("guild_id")
					.HasMaxLength(19);

				entity.Property(e => e.UserId)
					.HasColumnName("user_id")
					.HasMaxLength(19);

				entity.Property(e => e.PointCount).HasColumnName("point_count");
			});

			modelBuilder.Entity<Moderation>(entity =>
			{
				entity.HasKey(e => new {e.GuildId, e.CaseId})
					.HasName("moderation_guild_case_idx");

				entity.ToTable("moderation");

				entity.Property(e => e.GuildId)
					.HasColumnName("guild_id")
					.HasMaxLength(19);

				entity.Property(e => e.CaseId).HasColumnName("case_id");

				entity.Property(e => e.CreatedAt).HasColumnName("created_at");

				entity.Property(e => e.Duration).HasColumnName("duration");

				entity.Property(e => e.ExtraData)
					.HasColumnName("extra_data")
					.HasColumnType("json");

				entity.Property(e => e.ModeratorId)
					.HasColumnName("moderator_id")
					.HasMaxLength(19);

				entity.Property(e => e.Reason)
					.HasColumnName("reason")
					.HasMaxLength(2000);

				entity.Property(e => e.Type).HasColumnName("type");

				entity.Property(e => e.UserId)
					.HasColumnName("user_id")
					.HasMaxLength(19);
			});

			modelBuilder.Entity<Starboard>(entity =>
			{
				entity.HasKey(e => new {e.GuildId, e.MessageId})
					.HasName("starboard_guild_message_idx");

				entity.ToTable("starboard");

				entity.Property(e => e.GuildId)
					.HasColumnName("guild_id")
					.HasMaxLength(19);

				entity.Property(e => e.MessageId)
					.HasColumnName("message_id")
					.HasMaxLength(19);

				entity.Property(e => e.ChannelId)
					.IsRequired()
					.HasColumnName("channel_id")
					.HasMaxLength(19);

				entity.Property(e => e.Enabled).HasColumnName("enabled");

				entity.Property(e => e.StarMessageId)
					.HasColumnName("star_message_id")
					.HasMaxLength(19);

				entity.Property(e => e.Stars).HasColumnName("stars");

				entity.Property(e => e.UserId)
					.IsRequired()
					.HasColumnName("user_id")
					.HasMaxLength(19);
			});

			modelBuilder.Entity<TwitchStreamSubscriptions>(entity =>
			{
				entity.ToTable("twitch_stream_subscriptions");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasMaxLength(16);

				entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");

				entity.Property(e => e.GuildIds)
					.IsRequired()
					.HasColumnName("guild_ids")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("ARRAY[]::character varying[]");

				entity.Property(e => e.IsStreaming).HasColumnName("is_streaming");
			});

			modelBuilder.Entity<Users>(entity =>
			{
				entity.ToTable("users");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasMaxLength(19);

				entity.Property(e => e.BadgeList)
					.IsRequired()
					.HasColumnName("badge_list")
					.HasColumnType("character varying(6)[]")
					.HasDefaultValueSql("'{}'::character varying(6)[]");

				entity.Property(e => e.BadgeSet)
					.IsRequired()
					.HasColumnName("badge_set")
					.HasColumnType("character varying(6)[]")
					.HasDefaultValueSql("'{}'::character varying(6)[]");

				entity.Property(e => e.BannerList)
					.IsRequired()
					.HasColumnName("banner_list")
					.HasColumnType("character varying(6)[]")
					.HasDefaultValueSql("'{}'::character varying(6)[]");

				entity.Property(e => e.Color).HasColumnName("color");

				entity.Property(e => e.CommandUses).HasColumnName("command_uses");

				entity.Property(e => e.DarkTheme).HasColumnName("dark_theme");

				entity.Property(e => e.Marry)
					.IsRequired()
					.HasColumnName("marry")
					.HasColumnType("character varying(19)[]")
					.HasDefaultValueSql("'{}'::character varying(19)[]");

				entity.Property(e => e.ModerationDm)
					.IsRequired()
					.HasColumnName("moderation_dm")
					.HasDefaultValueSql("true");

				entity.Property(e => e.Money).HasColumnName("money");

				entity.Property(e => e.NextDaily).HasColumnName("next_daily");

				entity.Property(e => e.NextReputation).HasColumnName("next_reputation");

				entity.Property(e => e.PointCount).HasColumnName("point_count");

				entity.Property(e => e.ReputationCount).HasColumnName("reputation_count");

				entity.Property(e => e.ThemeLevel)
					.IsRequired()
					.HasColumnName("theme_level")
					.HasMaxLength(6)
					.HasDefaultValueSql("'1001'::character varying");

				entity.Property(e => e.ThemeProfile)
					.IsRequired()
					.HasColumnName("theme_profile")
					.HasMaxLength(6)
					.HasDefaultValueSql("'0001'::character varying");

				entity.Property(e => e.Vault).HasColumnName("vault");
			});
		}
	}
}

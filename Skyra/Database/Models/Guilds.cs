﻿using System;
using System.Collections.Generic;

namespace Skyra.Database.Models
{
    public class Guilds
    {
        public string Id { get; set; }
        public string Prefix { get; set; }
        public string Language { get; set; }
        public bool DisableNaturalPrefix { get; set; }
        public string[] DisabledCommands { get; set; }
        public int CommandUses { get; set; }
        public string[] Tags { get; set; }
        public string[] PermissionsUsers { get; set; }
        public string[] PermissionsRoles { get; set; }
        public string ChannelsAnnouncements { get; set; }
        public string ChannelsGreeting { get; set; }
        public string ChannelsFarewell { get; set; }
        public string ChannelsMemberLogs { get; set; }
        public string ChannelsMessageLogs { get; set; }
        public string ChannelsModerationLogs { get; set; }
        public string ChannelsNsfwMessageLogs { get; set; }
        public string ChannelsImageLogs { get; set; }
        public string ChannelsPruneLogs { get; set; }
        public string ChannelsReactionLogs { get; set; }
        public string ChannelsRoles { get; set; }
        public string ChannelsSpam { get; set; }
        public string[] CommandAutodelete { get; set; }
        public string[] DisabledChannels { get; set; }
        public string[] DisabledCommandsChannels { get; set; }
        public bool EventsBanAdd { get; set; }
        public bool EventsBanRemove { get; set; }
        public bool EventsMemberAdd { get; set; }
        public bool EventsMemberRemove { get; set; }
        public bool EventsMemberNameUpdate { get; set; }
        public bool EventsMessageDelete { get; set; }
        public bool EventsMessageEdit { get; set; }
        public bool EventsTwemojiReactions { get; set; }
        public string MessagesFarewell { get; set; }
        public string MessagesGreeting { get; set; }
        public string MessagesJoinDm { get; set; }
        public string[] MessagesIgnoreChannels { get; set; }
        public bool MessagesModerationDm { get; set; }
        public bool? MessagesModerationReasonDisplay { get; set; }
        public bool? MessagesModerationMessageDisplay { get; set; }
        public bool MessagesModerationAutoDelete { get; set; }
        public bool? MessagesModeratorNameDisplay { get; set; }
        public string[] StickyRoles { get; set; }
        public string RolesAdmin { get; set; }
        public string[] RolesAuto { get; set; }
        public string RolesInitial { get; set; }
        public string RolesMessageReaction { get; set; }
        public string RolesModerator { get; set; }
        public string RolesMuted { get; set; }
        public string RolesRestrictedReaction { get; set; }
        public string RolesRestrictedEmbed { get; set; }
        public string RolesRestrictedAttachment { get; set; }
        public string RolesRestrictedVoice { get; set; }
        public string[] RolesPublic { get; set; }
        public string[] RolesReactions { get; set; }
        public bool RolesRemoveInitial { get; set; }
        public string RolesStaff { get; set; }
        public string RolesDj { get; set; }
        public string RolesSubscriber { get; set; }
        public string[] RolesUniqueRoleSets { get; set; }
        public bool SelfmodAttachment { get; set; }
        public short SelfmodAttachmentMaximum { get; set; }
        public short SelfmodAttachmentDuration { get; set; }
        public short SelfmodAttachmentAction { get; set; }
        public int? SelfmodAttachmentPunishmentDuration { get; set; }
        public bool SelfmodCapitalsEnabled { get; set; }
        public short SelfmodCapitalsMinimum { get; set; }
        public short SelfmodCapitalsMaximum { get; set; }
        public short SelfmodCapitalsSoftAction { get; set; }
        public short SelfmodCapitalsHardAction { get; set; }
        public int? SelfmodCapitalsHardActionDuration { get; set; }
        public short SelfmodCapitalsThresholdMaximum { get; set; }
        public int SelfmodCapitalsThresholdDuration { get; set; }
        public string[] SelfmodLinksWhitelist { get; set; }
        public bool SelfmodLinksEnabled { get; set; }
        public short SelfmodLinksSoftAction { get; set; }
        public short SelfmodLinksHardAction { get; set; }
        public int? SelfmodLinksHardActionDuration { get; set; }
        public short SelfmodLinksThresholdMaximum { get; set; }
        public int SelfmodLinksThresholdDuration { get; set; }
        public bool SelfmodMessagesEnabled { get; set; }
        public short SelfmodMessagesMaximum { get; set; }
        public short SelfmodMessagesQueueSize { get; set; }
        public short SelfmodMessagesSoftAction { get; set; }
        public short SelfmodMessagesHardAction { get; set; }
        public int? SelfmodMessagesHardActionDuration { get; set; }
        public short SelfmodMessagesThresholdMaximum { get; set; }
        public int SelfmodMessagesThresholdDuration { get; set; }
        public bool SelfmodNewlinesEnabled { get; set; }
        public short SelfmodNewlinesMaximum { get; set; }
        public short SelfmodNewlinesSoftAction { get; set; }
        public short SelfmodNewlinesHardAction { get; set; }
        public int? SelfmodNewlinesHardActionDuration { get; set; }
        public short SelfmodNewlinesThresholdMaximum { get; set; }
        public int SelfmodNewlinesThresholdDuration { get; set; }
        public bool SelfmodInvitesEnabled { get; set; }
        public short SelfmodInvitesSoftAction { get; set; }
        public short SelfmodInvitesHardAction { get; set; }
        public int? SelfmodInvitesHardActionDuration { get; set; }
        public short SelfmodInvitesThresholdMaximum { get; set; }
        public int SelfmodInvitesThresholdDuration { get; set; }
        public bool SelfmodFilterEnabled { get; set; }
        public short SelfmodFilterSoftAction { get; set; }
        public short SelfmodFilterHardAction { get; set; }
        public int? SelfmodFilterHardActionDuration { get; set; }
        public short SelfmodFilterThresholdMaximum { get; set; }
        public int SelfmodFilterThresholdDuration { get; set; }
        public string[] SelfmodFilterRaw { get; set; }
        public bool SelfmodReactionsEnabled { get; set; }
        public short SelfmodReactionsMaximum { get; set; }
        public string[] SelfmodReactionsWhitelist { get; set; }
        public string[] SelfmodReactionsBlacklist { get; set; }
        public short SelfmodReactionsSoftAction { get; set; }
        public short SelfmodReactionsHardAction { get; set; }
        public int? SelfmodReactionsHardActionDuration { get; set; }
        public short SelfmodReactionsThresholdMaximum { get; set; }
        public int SelfmodReactionsThresholdDuration { get; set; }
        public bool SelfmodRaid { get; set; }
        public short SelfmodRaidthreshold { get; set; }
        public string[] SelfmodIgnoreChannels { get; set; }
        public bool NoMentionSpamEnabled { get; set; }
        public bool NoMentionSpamAlerts { get; set; }
        public short NoMentionSpamMentionsAllowed { get; set; }
        public int NoMentionSpamTimePeriod { get; set; }
        public bool SocialAchieve { get; set; }
        public string SocialAchieveMessage { get; set; }
        public string[] SocialIgnoreChannels { get; set; }
        public string StarboardChannel { get; set; }
        public string StarboardEmoji { get; set; }
        public string[] StarboardIgnoreChannels { get; set; }
        public short StarboardMinimum { get; set; }
        public string[] TriggerAlias { get; set; }
        public string[] TriggerIncludes { get; set; }
        public string[] NotificationsStreamsTwitchStreamers { get; set; }
        public string[] SelfmodCapitalsIgnoredRoles { get; set; }
        public string[] SelfmodCapitalsIgnoredChannels { get; set; }
        public string[] SelfmodLinksIgnoredRoles { get; set; }
        public string[] SelfmodLinksIgnoredChannels { get; set; }
        public string[] SelfmodMessagesIgnoredRoles { get; set; }
        public string[] SelfmodMessagesIgnoredChannels { get; set; }
        public string[] SelfmodNewlinesIgnoredRoles { get; set; }
        public string[] SelfmodNewlinesIgnoredChannels { get; set; }
        public string[] SelfmodInvitesIgnoredRoles { get; set; }
        public string[] SelfmodInvitesIgnoredChannels { get; set; }
        public string[] SelfmodFilterIgnoredRoles { get; set; }
        public string[] SelfmodFilterIgnoredChannels { get; set; }
        public string[] SelfmodReactionsIgnoredRoles { get; set; }
        public string[] SelfmodReactionsIgnoredChannels { get; set; }
        public bool? MusicAllowStreams { get; set; }
        public double MusicDefaultVolume { get; set; }
        public double MusicMaximumDuration { get; set; }
        public double MusicMaximumEntriesPerUser { get; set; }
        public bool? SocialEnabled { get; set; }
        public double SocialMultiplier { get; set; }
    }
}

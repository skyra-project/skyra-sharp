using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Skyra.Core.Utils;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models.Prompts
{
	public sealed class RichDisplay : IPromptData, IPromptDataReaction
	{
		public RichDisplay(ulong authorId, ulong messageId)
		{
			AuthorId = authorId;
			MessageId = messageId;
			InternalTemplate = new MessageEmbed();
			InformationPage = null;
			AllowedEmojis = new (RichDisplayReactionType, string)[0];
			Emojis = new RichDisplayEmojis();
			FooterEnabled = false;
			FooterPrefix = null;
			FooterSuffix = null;
			Pages = new List<MessageEmbed>();
			PagePosition = 0;
		}

		[JsonConstructor]
		public RichDisplay(ulong authorId, ulong messageId, List<MessageEmbed> pages,
			MessageEmbed? informationPage, int pagePosition, (RichDisplayReactionType, string)[] allowedEmojis)
		{
			AuthorId = authorId;
			MessageId = messageId;
			AllowedEmojis = allowedEmojis;
			InternalTemplate = new MessageEmbed();
			InformationPage = informationPage;
			Emojis = new RichDisplayEmojis();
			FooterEnabled = false;
			FooterPrefix = null;
			FooterSuffix = null;
			Pages = pages;
			PagePosition = pagePosition;
		}

		[JsonProperty("ip")]
		public MessageEmbed? InformationPage { get; set; }

		[JsonProperty("ps")]
		public List<MessageEmbed> Pages { get; set; }

		[JsonProperty("ae")]
		public (RichDisplayReactionType, string)[] AllowedEmojis { get; set; }

		[JsonProperty("pp")]
		public int PagePosition { get; set; }

		[JsonIgnore]
		private MessageEmbed InternalTemplate { get; set; }

		[JsonIgnore]
		[NotNull]
		public MessageEmbed Template
		{
			get => new MessageEmbed(InternalTemplate);
			set => InternalTemplate = value;
		}

		[JsonIgnore]
		public RichDisplayEmojis Emojis { get; set; }

		[JsonIgnore]
		public bool FooterEnabled { get; set; }

		[JsonIgnore]
		public string? FooterPrefix { get; set; }

		[JsonIgnore]
		public string? FooterSuffix { get; set; }

		private IEnumerable<DiscordApiErrorCodes> IgnoreMessageUpdateCodes { get; } = new[]
		{
			DiscordApiErrorCodes.UnknownMessage, DiscordApiErrorCodes.UnknownChannel,
			DiscordApiErrorCodes.UnknownGuild
		};

		private IEnumerable<DiscordApiErrorCodes> IgnoreReactionRemoveCodes { get; } = new[]
		{
			DiscordApiErrorCodes.UnknownMessage, DiscordApiErrorCodes.UnknownChannel,
			DiscordApiErrorCodes.UnknownGuild, DiscordApiErrorCodes.UnknownEmoji
		};

		[NotNull]
		public string ToKey()
		{
			return IPromptDataReaction.ToKey(MessageId, AuthorId);
		}

		[JsonProperty("aid")]
		public ulong AuthorId { get; }

		[JsonProperty("mid")]
		public ulong MessageId { get; set; }

		public async Task<TimeSpan?> RunAsync([NotNull] MessageReactionAddPayload reaction)
		{
			var emoji = Utilities.ResolveEmoji(reaction.Emoji);
			var action = RetrieveEntry(emoji);
			switch (action)
			{
				case RichDisplayReactionType.None:
					return TimeSpan.Zero;
				case RichDisplayReactionType.First:
					if (PagePosition == 0) return TimeSpan.Zero;
					PagePosition = 0;
					if (await Render(reaction.ChannelId)) return TimeSpan.FromMinutes(10);
					return null;
				case RichDisplayReactionType.Back:
					if (PagePosition == 0) return TimeSpan.Zero;
					--PagePosition;
					if (await Render(reaction.ChannelId)) return TimeSpan.FromMinutes(10);
					return null;
				case RichDisplayReactionType.Forward:
					if (PagePosition == Pages.Count - 1) return TimeSpan.Zero;
					++PagePosition;
					if (await Render(reaction.ChannelId)) return TimeSpan.FromMinutes(10);
					return null;
				case RichDisplayReactionType.Last:
					if (PagePosition == Pages.Count - 1) return TimeSpan.Zero;
					PagePosition = Pages.Count - 1;
					if (await Render(reaction.ChannelId)) return TimeSpan.FromMinutes(10);
					return null;
				case RichDisplayReactionType.Info:
					if (PagePosition == -1) return TimeSpan.Zero;
					PagePosition = -1;
					if (await Render(reaction.ChannelId)) return TimeSpan.FromMinutes(10);
					return null;
				case RichDisplayReactionType.Stop:
					// await RemoveReactions(reaction.ChannelId);
					return null;
				case RichDisplayReactionType.Jump:
					// TODO(kyranet): Create text prompt to ask for the number
					// TODO(kyranet): Render
					return TimeSpan.FromMinutes(10);
				default:
					throw new ArgumentOutOfRangeException(nameof(action));
			}
		}

		[NotNull]
		public RichDisplay SetEmojis(RichDisplayEmojis emojis)
		{
			Emojis = emojis;
			return this;
		}

		[NotNull]
		public RichDisplay SetFooterPrefix(string prefix)
		{
			FooterEnabled = false;
			FooterPrefix = prefix;
			return this;
		}

		[NotNull]
		public RichDisplay SetFooterSuffix(string suffix)
		{
			FooterEnabled = false;
			FooterSuffix = suffix;
			return this;
		}

		[NotNull]
		public RichDisplay UseCustomFooters()
		{
			FooterEnabled = true;
			return this;
		}

		[NotNull]
		public RichDisplay AddPage(MessageEmbed embed)
		{
			Pages.Add(embed);
			return this;
		}

		[NotNull]
		public RichDisplay AddPage([NotNull] Func<MessageEmbed, MessageEmbed> callback)
		{
			var value = callback(Template);
			Pages.Add(value);
			return this;
		}

		[NotNull]
		public RichDisplay SetInformationPage(MessageEmbed? embed)
		{
			InformationPage = embed;
			return this;
		}

		private async Task<bool> RemoveReactions(string channelId)
		{
			var reactionDeleteQuery =
				IClient.Instance.Rest.Channels[channelId].Messages[MessageId.ToString()].Reactions.DeleteAsync();

			return await Utilities.ResolveAsBooleanOnErrorCodes(reactionDeleteQuery, IgnoreMessageUpdateCodes);
		}

		private async Task<bool> Render(string channelId)
		{
			var messageUpdateQuery = IClient.Instance.Rest.Channels[channelId]
				.Messages[MessageId.ToString()].PatchAsync(new SendableMessage
				{
					Embed = PagePosition == -1 ? InformationPage : Pages[PagePosition]
				});

			return await Utilities.ResolveAsBooleanOnErrorCodes(messageUpdateQuery, IgnoreMessageUpdateCodes);
		}

		private RichDisplayReactionType RetrieveEntry(string emoji)
		{
			foreach (var (type, allowedEmoji) in AllowedEmojis)
			{
				if (allowedEmoji == emoji) return type;
			}

			return RichDisplayReactionType.None;
		}

		[ItemNotNull]
		public async Task<RichDisplay> SetUpAsync([NotNull] Message message,
			RichDisplayRunOptions? options = null)
		{
			options ??= new RichDisplayRunOptions();
			PagePosition = options.StartPage;
			if (!FooterEnabled) SetFooters();

			if (message.AuthorId != message.Client.Id)
			{
				var sent = await message.SendAsync(options.MessageContent);
				MessageId = sent.Id;
			}

			var hasSetUp = await AddReactions(message, IterateEmojis(options.Stop, options.Jump, options.FirstLast));
			if (!hasSetUp) return this;

			await message.Client.Cache.Prompts.SetAsync(
				new PromptData(message.Client, PromptDataType.RichDisplay, this), options.Duration);
			await Render(message.ChannelId.ToString());
			return this;
		}

		private async Task<bool> AddReactions([NotNull] Message message,
			[NotNull] IEnumerable<(RichDisplayReactionType, string)> emojis)
		{
			var reacted = false;
			var messageId = MessageId.ToString();
			var channelId = message.ChannelId.ToString();
			AllowedEmojis = emojis.ToArray();
			foreach (var (_, emoji) in AllowedEmojis)
			{
				var query = message.Client.Rest.Channels[channelId].Messages[messageId].Reactions[emoji]["@me"]
					.PutAsync<object>(null);

				if (!await Utilities.ResolveAsBooleanOnErrorCodes(query, IgnoreReactionRemoveCodes))
				{
					return false;
				}

				reacted = true;
			}

			return reacted;
		}

		private IEnumerable<(RichDisplayReactionType, string)> IterateEmojis(bool stop, bool jump, bool firstLast)
		{
			if (Pages.Count > 1 || InformationPage != null)
			{
				if (firstLast)
				{
					if (Emojis.First != null) yield return (RichDisplayReactionType.First, Emojis.First);
					if (Emojis.Back != null) yield return (RichDisplayReactionType.Back, Emojis.Back);
					if (Emojis.Forward != null) yield return (RichDisplayReactionType.Forward, Emojis.Forward);
					if (Emojis.Last != null) yield return (RichDisplayReactionType.Last, Emojis.Last);
				}
				else
				{
					if (Emojis.Back != null) yield return (RichDisplayReactionType.Back, Emojis.Back);
					if (Emojis.Forward != null) yield return (RichDisplayReactionType.Forward, Emojis.Forward);
				}
			}

			if (Emojis.Info != null && InformationPage != null)
			{
				yield return (RichDisplayReactionType.Info, Emojis.Info);
			}

			// if (Emojis.Stop != null && stop) yield return (CoreRichDisplayReactionType.Stop, Emojis.Stop);
			// if (Emojis.Jump != null && jump) yield return (CoreRichDisplayReactionType.Jump, Emojis.Jump);
		}

		private void SetFooters()
		{
			for (var i = 0; i < Pages.Count; ++i)
			{
				Pages[i].SetFooter($"{FooterPrefix}{(i + 1).ToString()}/{Pages.Count.ToString()}{FooterSuffix}");
			}

			InformationPage?.SetFooter("ℹ");
		}
	}
}

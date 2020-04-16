using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Skyra.Core.Utils;
using Spectacles.NET.Rest.APIError;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models.Prompts
{
	public sealed class CoreRichDisplay : ICorePromptState, ICorePromptStateReaction
	{
		public CoreRichDisplay(ulong authorId, ulong messageId)
		{
			AuthorId = authorId;
			MessageId = messageId;
			InternalTemplate = new CoreMessageEmbed();
			InformationPage = null;
			AllowedEmojis = new (CoreRichDisplayReactionType, string)[0];
			Emojis = new CoreRichDisplayEmojis();
			FooterEnabled = false;
			FooterPrefix = null;
			FooterSuffix = null;
			Context = new CoreMessageEmbed[0];
			PagePosition = 0;
		}

		public CoreRichDisplay(ulong authorId, ulong messageId, CoreMessageEmbed[] context, CoreMessageEmbed?
			informationPage, int pagePosition, (CoreRichDisplayReactionType, string)[] allowedEmojis)
		{
			AuthorId = authorId;
			MessageId = messageId;
			AllowedEmojis = allowedEmojis;
			InternalTemplate = new CoreMessageEmbed();
			InformationPage = informationPage;
			Emojis = new CoreRichDisplayEmojis();
			FooterEnabled = false;
			FooterPrefix = null;
			FooterSuffix = null;
			Context = context ?? new CoreMessageEmbed[0];
			PagePosition = pagePosition;
		}

		[JsonProperty("ip")]
		public CoreMessageEmbed? InformationPage { get; set; }

		[JsonProperty("ctx")]
		public CoreMessageEmbed[] Context { get; set; }

		[JsonProperty("ae")]
		public (CoreRichDisplayReactionType, string)[] AllowedEmojis { get; set; }

		[JsonProperty("pp")]
		public int PagePosition { get; set; }

		[JsonIgnore]
		private CoreMessageEmbed InternalTemplate { get; set; }

		[JsonIgnore]
		[NotNull]
		public CoreMessageEmbed Template
		{
			get => new CoreMessageEmbed(InternalTemplate);
			set => InternalTemplate = value;
		}

		[JsonIgnore]
		public CoreRichDisplayEmojis Emojis { get; set; }

		[JsonIgnore]
		public bool FooterEnabled { get; set; }

		[JsonIgnore]
		public string? FooterPrefix { get; set; }

		[JsonIgnore]
		public string? FooterSuffix { get; set; }

		public string ToKey()
		{
			return ICorePromptStateReaction.ToKey(MessageId, AuthorId);
		}

		[JsonProperty("aid")]
		public ulong AuthorId { get; }

		[JsonProperty("mid")]
		public ulong MessageId { get; set; }

		public async Task<TimeSpan?> RunAsync(MessageReactionAddPayload reaction)
		{
			var action = RetrieveEntry(Utilities.ResolveEmoji(reaction.Emoji));
			switch (action)
			{
				case CoreRichDisplayReactionType.None:
					return TimeSpan.Zero;
				case CoreRichDisplayReactionType.First:
					if (PagePosition == 0) return TimeSpan.Zero;
					PagePosition = 0;
					if (await Render(reaction.ChannelId)) return TimeSpan.FromMinutes(10);
					return null;
				case CoreRichDisplayReactionType.Back:
					if (PagePosition == 0) return TimeSpan.Zero;
					--PagePosition;
					if (await Render(reaction.ChannelId)) return TimeSpan.FromMinutes(10);
					return null;
				case CoreRichDisplayReactionType.Forward:
					if (PagePosition == Context.Length - 1) return TimeSpan.Zero;
					++PagePosition;
					if (await Render(reaction.ChannelId)) return TimeSpan.FromMinutes(10);
					return null;
				case CoreRichDisplayReactionType.Last:
					if (PagePosition == Context.Length - 1) return TimeSpan.Zero;
					PagePosition = Context.Length - 1;
					if (await Render(reaction.ChannelId)) return TimeSpan.FromMinutes(10);
					return null;
				case CoreRichDisplayReactionType.Info:
					if (PagePosition == -1) return TimeSpan.Zero;
					PagePosition = -1;
					if (await Render(reaction.ChannelId)) return TimeSpan.FromMinutes(10);
					return null;
				case CoreRichDisplayReactionType.Stop:
					return null;
				case CoreRichDisplayReactionType.Jump:
					// TODO(kyranet): Create text prompt to ask for the number
					// TODO(kyranet): Render
					return TimeSpan.FromMinutes(10);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		[NotNull]
		public CoreRichDisplay SetEmojis(CoreRichDisplayEmojis emojis)
		{
			Emojis = emojis;
			return this;
		}

		[NotNull]
		public CoreRichDisplay SetFooterPrefix(string prefix)
		{
			FooterEnabled = false;
			FooterPrefix = prefix;
			return this;
		}

		[NotNull]
		public CoreRichDisplay SetFooterSuffix(string suffix)
		{
			FooterEnabled = false;
			FooterSuffix = suffix;
			return this;
		}

		[NotNull]
		public CoreRichDisplay UseCustomFooters()
		{
			FooterEnabled = true;
			return this;
		}

		[NotNull]
		public CoreRichDisplay AddPage(CoreMessageEmbed embed)
		{
			Context = Context.Append(embed).ToArray();
			return this;
		}

		[NotNull]
		public CoreRichDisplay AddPage([NotNull] Func<CoreMessageEmbed, CoreMessageEmbed> callback)
		{
			var value = callback(Template);
			Context = Context.Append(value).ToArray();
			return this;
		}

		[NotNull]
		public CoreRichDisplay SetInformationPage(CoreMessageEmbed? embed)
		{
			InformationPage = embed;
			return this;
		}

		private async Task<bool> Render(string channelId)
		{
			try
			{
				await IClient.Instance.Rest.Channels[channelId].Messages[MessageId.ToString()].PatchAsync<object>(
					new SendableMessage
					{
						Embed = PagePosition == -1 ? InformationPage : Context[PagePosition]
					});

				return true;
			}
			catch (DiscordAPIException exception)
			{
				if (exception.ErrorCode is null) throw;
				return (DiscordApiErrorCodes) exception.ErrorCode! switch
				{
					DiscordApiErrorCodes.UnknownMessage => false,
					DiscordApiErrorCodes.UnknownChannel => false,
					DiscordApiErrorCodes.UnknownGuild => false,
					_ => throw exception
				};
			}
		}

		private CoreRichDisplayReactionType RetrieveEntry(string emoji)
		{
			foreach (var (type, allowedEmoji) in AllowedEmojis)
			{
				if (allowedEmoji == emoji) return type;
			}

			return CoreRichDisplayReactionType.None;
		}

		[ItemNotNull]
		public async Task<CoreRichDisplay> SetUpAsync([NotNull] CoreMessage message,
			CoreRichDisplayRunOptions? options = null)
		{
			options ??= new CoreRichDisplayRunOptions();
			PagePosition = options.StartPage;
			if (!FooterEnabled) SetFooters();

			if (message.AuthorId == message.Client.Id)
			{
				await message.EditAsync(new SendableMessage
				{
					Embed = Context[PagePosition]
				});
				MessageId = message.Id;
			}
			else
			{
				var sent = await message.SendAsync(new SendableMessage
				{
					Embed = Context[PagePosition]
				});
				MessageId = sent.Id;
			}

			var hasSetUp = await AddReactions(message, IterateEmojis(options.Stop, options.Jump, options.FirstLast));
			if (!hasSetUp) return this;

			await message.Client.Cache.Prompts.SetAsync(
				new CorePromptState(message.Client, CorePromptStateType.RichDisplay, this), options.Duration);
			return this;
		}

		private async Task<bool> AddReactions([NotNull] CoreMessage message,
			[NotNull] IEnumerable<(CoreRichDisplayReactionType, string)> emojis)
		{
			var reacted = false;
			var messageId = MessageId.ToString();
			var channelId = message.ChannelId.ToString();
			AllowedEmojis = emojis.ToArray();
			foreach (var (_, emoji) in AllowedEmojis)
			{
				try
				{
					await message.Client.Rest.Channels[channelId].Messages[messageId].Reactions[emoji]["@me"]
						.PutAsync<object>(null);
				}
				catch (DiscordAPIException exception)
				{
					if (exception.ErrorCode is null) return false;
					return (DiscordApiErrorCodes) exception.ErrorCode! switch
					{
						DiscordApiErrorCodes.UnknownMessage => false,
						DiscordApiErrorCodes.UnknownChannel => false,
						DiscordApiErrorCodes.UnknownGuild => false,
						_ => throw exception
					};
				}

				reacted = true;
			}

			return reacted;
		}

		private IEnumerable<(CoreRichDisplayReactionType, string)> IterateEmojis(bool stop, bool jump, bool firstLast)
		{
			if (Context.Length > 1 || InformationPage != null)
			{
				if (firstLast)
				{
					if (Emojis.First != null) yield return (CoreRichDisplayReactionType.First, Emojis.First);
					if (Emojis.Back != null) yield return (CoreRichDisplayReactionType.Back, Emojis.Back);
					if (Emojis.Forward != null) yield return (CoreRichDisplayReactionType.Forward, Emojis.Forward);
					if (Emojis.Last != null) yield return (CoreRichDisplayReactionType.Last, Emojis.Last);
				}
				else
				{
					if (Emojis.Back != null) yield return (CoreRichDisplayReactionType.Back, Emojis.Back);
					if (Emojis.Forward != null) yield return (CoreRichDisplayReactionType.Forward, Emojis.Forward);
				}
			}

			if (Emojis.Info != null && InformationPage != null)
			{
				yield return (CoreRichDisplayReactionType.Info, Emojis.Info);
			}

			if (Emojis.Stop != null && stop) yield return (CoreRichDisplayReactionType.Stop, Emojis.Stop);
			if (Emojis.Jump != null && jump) yield return (CoreRichDisplayReactionType.Jump, Emojis.Jump);
		}

		private void SetFooters()
		{
			for (var i = 0; i < Context.Length; ++i)
			{
				Context[i].SetFooter($"{FooterPrefix}{(i + 1).ToString()}/{Context.Length.ToString()}{FooterSuffix}");
			}

			InformationPage?.SetFooter("ℹ");
		}
	}
}

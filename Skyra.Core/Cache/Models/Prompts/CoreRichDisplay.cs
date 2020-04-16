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
	public sealed class CoreRichDisplay : CorePromptStateReaction
	{
		public CoreRichDisplay(ulong authorId, ulong messageId, CoreMessageEmbed[] context = null, CoreMessageEmbed?
			informationPage = null, CoreRichDisplayEmojis? emojis = null) : base(authorId, messageId, null)
		{
			InternalTemplate = new CoreMessageEmbed();
			InformationPage = informationPage;
			Emojis = emojis ?? new CoreRichDisplayEmojis();
			FooterEnabled = false;
			FooterPrefix = null;
			FooterSuffix = null;
			Context = context ?? new CoreMessageEmbed[0];
		}

		[JsonIgnore]
		private CoreMessageEmbed InternalTemplate { get; set; }

		[JsonIgnore]
		[NotNull]
		public CoreMessageEmbed Template
		{
			get => new CoreMessageEmbed(InternalTemplate);
			set => InternalTemplate = value;
		}

		[JsonProperty("ip")]
		public CoreMessageEmbed? InformationPage { get; set; }

		[JsonProperty("e")]
		public CoreRichDisplayEmojis Emojis { get; set; }

		[JsonIgnore]
		public bool FooterEnabled { get; set; }

		[JsonIgnore]
		public string? FooterPrefix { get; set; }

		[JsonIgnore]
		public string? FooterSuffix { get; set; }

		[JsonProperty("ctx")]
		public new CoreMessageEmbed[] Context { get; set; }

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
		public CoreRichDisplay AddPage(Embed embed)
		{
			Context.Append(embed);
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

		[ItemNotNull]
		public async Task<CoreRichDisplay> RunAsync([NotNull] CoreMessage message,
			CoreRichDisplayRunOptions options = new CoreRichDisplayRunOptions())
		{
			if (!FooterEnabled) SetFooters();

			if (message.AuthorId == message.Client.Id)
			{
				await message.EditAsync(new SendableMessage
				{
					Embed = Context[options.StartPage]
				});
				MessageId = message.Id;
			}
			else
			{
				var sent = await message.SendAsync(new SendableMessage
				{
					Embed = Context[options.StartPage]
				});
				MessageId = sent.Id;
			}

			var hasSetUp = await AddReactions(message, IterateEmojis(options.Stop, options.Jump, options.FirstLast));
			if (!hasSetUp) return this;

			await message.Client.Cache.Prompts.SetAsync(
				new CorePromptState(message.Client, CorePromptStateType.RichDisplay, this), options.Duration);
			return this;
		}

		private async Task<bool> AddReactions([NotNull] CoreMessage message, [NotNull] IEnumerable<string> emojis)
		{
			var reacted = false;
			var messageId = MessageId.ToString();
			var channelId = message.ChannelId.ToString();
			foreach (var emoji in emojis)
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

		private IEnumerable<string> IterateEmojis(bool stop, bool jump, bool firstLast)
		{
			if (Context.Length > 1 || InformationPage != null)
			{
				if (firstLast)
				{
					yield return Emojis.First;
					yield return Emojis.Back;
					yield return Emojis.Forward;
					yield return Emojis.Last;
				}
				else
				{
					yield return Emojis.Back;
					yield return Emojis.Forward;
				}
			}

			if (InformationPage != null) yield return Emojis.Info;
			if (stop) yield return Emojis.Stop;
			if (jump) yield return Emojis.Jump;
		}

		private void SetFooters()
		{
			for (var i = 0; i < Context.Length; ++i)
			{
				Context[i].SetFooter($"{FooterPrefix}{(i + 1).ToString()}/{Context.Length}{FooterSuffix}");
			}

			InformationPage?.SetFooter("ℹ");
		}
	}
}

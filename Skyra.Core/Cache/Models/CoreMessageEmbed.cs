using System;
using System.Linq;
using JetBrains.Annotations;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	/// <inheritdoc />
	public sealed class CoreMessageEmbed : Embed
	{
		public CoreMessageEmbed()
		{
		}

		public CoreMessageEmbed([NotNull] Embed embed)
		{
			Title = embed.Title;
			Type = embed.Type;
			Description = embed.Description;
			URL = embed.URL;
			Timestamp = embed.Timestamp;
			Color = embed.Color;
			Footer = embed.Footer;
			Image = embed.Image;
			Thumbnail = embed.Thumbnail;
			Video = embed.Video;
			Provider = embed.Provider;
			Author = embed.Author;
			Fields = embed.Fields;
		}

		public DateTime? CreatedAt => Timestamp;
		public long? CreatedTimestamp => Timestamp?.ToBinary();

		[CanBeNull]
		public string? HexColor => Color?.ToString("x6");

		[NotNull]
		public CoreMessageEmbed AddField(string name, string value, bool inline = false)
		{
			Fields.Append(new EmbedField
			{
				Name = name,
				Value = value,
				Inline = inline
			});
			return this;
		}

		[NotNull]
		public CoreMessageEmbed AddBlankField(bool inline = false)
		{
			return AddField("\u200B", "\u200B", inline);
		}

		[NotNull]
		public CoreMessageEmbed SetAuthor(string name, string? iconUrl = null, string? url = null)
		{
			return SetAuthor(new EmbedAuthor
			{
				Name = name,
				IconURL = iconUrl,
				URL = url
			});
		}

		[NotNull]
		public CoreMessageEmbed SetAuthor(EmbedAuthor? author = null)
		{
			Author = author;
			return this;
		}

		[NotNull]
		public CoreMessageEmbed SetColor(int color)
		{
			Color = color;
			return this;
		}

		[NotNull]
		public CoreMessageEmbed SetFooter(string text, string? iconUrl = null)
		{
			return SetFooter(new EmbedFooter
			{
				Text = text,
				IconURL = iconUrl
			});
		}

		[NotNull]
		public CoreMessageEmbed SetFooter(EmbedFooter? footer = null)
		{
			Footer = footer;
			return this;
		}

		[NotNull]
		public CoreMessageEmbed SetImage(string url)
		{
			return SetImage(new EmbedImage
			{
				URL = url
			});
		}

		[NotNull]
		public CoreMessageEmbed SetImage(EmbedImage? image = null)
		{
			Image = image;
			return this;
		}

		[NotNull]
		public CoreMessageEmbed SetThumbnail(string url)
		{
			return SetThumbnail(new EmbedThumbnail
			{
				URL = url
			});
		}

		[NotNull]
		public CoreMessageEmbed SetThumbnail(EmbedThumbnail? thumbnail = null)
		{
			Thumbnail = thumbnail;
			return this;
		}

		[NotNull]
		public CoreMessageEmbed UpdateTimestamp()
		{
			Timestamp = DateTime.Now;
			return this;
		}

		[NotNull]
		public CoreMessageEmbed SetTitle(string? title = null)
		{
			Title = title;
			return this;
		}

		[NotNull]
		public CoreMessageEmbed SetDescription(string? description = null)
		{
			Description = description;
			return this;
		}

		[NotNull]
		public CoreMessageEmbed SetUrl(string? url = null)
		{
			URL = url;
			return this;
		}
	}
}

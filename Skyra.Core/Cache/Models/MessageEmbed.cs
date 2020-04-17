using System;
using System.Linq;
using JetBrains.Annotations;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	/// <inheritdoc />
	public sealed class MessageEmbed : Embed
	{
		public MessageEmbed()
		{
		}

		public MessageEmbed([NotNull] Embed embed)
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
		public MessageEmbed AddField(string name, string value, bool inline = false)
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
		public MessageEmbed AddBlankField(bool inline = false)
		{
			return AddField("\u200B", "\u200B", inline);
		}

		[NotNull]
		public MessageEmbed SetAuthor(string name, string? iconUrl = null, string? url = null)
		{
			return SetAuthor(new EmbedAuthor
			{
				Name = name,
				IconURL = iconUrl,
				URL = url
			});
		}

		[NotNull]
		public MessageEmbed SetAuthor(EmbedAuthor? author = null)
		{
			Author = author;
			return this;
		}

		[NotNull]
		public MessageEmbed SetColor(int color)
		{
			Color = color;
			return this;
		}

		[NotNull]
		public MessageEmbed SetFooter(string text, string? iconUrl = null)
		{
			return SetFooter(new EmbedFooter
			{
				Text = text,
				IconURL = iconUrl
			});
		}

		[NotNull]
		public MessageEmbed SetFooter(EmbedFooter? footer = null)
		{
			Footer = footer;
			return this;
		}

		[NotNull]
		public MessageEmbed SetImage(string url)
		{
			return SetImage(new EmbedImage
			{
				URL = url
			});
		}

		[NotNull]
		public MessageEmbed SetImage(EmbedImage? image = null)
		{
			Image = image;
			return this;
		}

		[NotNull]
		public MessageEmbed SetThumbnail(string url)
		{
			return SetThumbnail(new EmbedThumbnail
			{
				URL = url
			});
		}

		[NotNull]
		public MessageEmbed SetThumbnail(EmbedThumbnail? thumbnail = null)
		{
			Thumbnail = thumbnail;
			return this;
		}

		[NotNull]
		public MessageEmbed UpdateTimestamp()
		{
			Timestamp = DateTime.Now;
			return this;
		}

		[NotNull]
		public MessageEmbed SetTitle(string? title = null)
		{
			Title = title;
			return this;
		}

		[NotNull]
		public MessageEmbed SetDescription(string? description = null)
		{
			Description = description;
			return this;
		}

		[NotNull]
		public MessageEmbed SetUrl(string? url = null)
		{
			URL = url;
			return this;
		}
	}
}

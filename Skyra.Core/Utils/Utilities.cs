using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Spectacles.NET.Rest.APIError;
using Spectacles.NET.Types;

namespace Skyra.Core.Utils
{
	public static class Utilities
	{
		private static readonly Regex RegexFullCustomEmoji =
			new Regex(@"^<a?:\w{2,32}:\d{17,18}>$", RegexOptions.Compiled);

		private static readonly Regex RegexPartialCustomEmoji =
			new Regex(@"^a?:\w{2,32}:\d{17,18}$", RegexOptions.Compiled);

		private static readonly Regex RegexEmojiNameArtifact = new Regex(@"~\d+");

		[NotNull]
		public static string CodeBlock(string language, [NotNull] string content)
		{
			var escaped = content.Replace(@"""""""", @"\""\""\""");
			return $"```{language}\n{escaped}\n```";
		}

		[CanBeNull]
		public static string? ResolveEmoji([NotNull] string emoji)
		{
			if (RegexFullCustomEmoji.IsMatch(emoji)) return emoji.Substring(1, -1);
			if (RegexPartialCustomEmoji.IsMatch(emoji)) return emoji;
			// TODO(kyranet): Support unicode
			return null;
		}

		[NotNull]
		public static string ResolveEmoji([NotNull] Emoji emoji)
		{
			if (emoji.Id is null) return Uri.EscapeDataString(emoji.Name);
			var animated = emoji.Animated == true ? "a" : "n";
			var name = emoji.Name.Replace(RegexEmojiNameArtifact, groups => "");
			var id = emoji.Id;
			return $"{animated}:{name}:{id}";
		}

		public static async Task<T?> ResolveOnErrorCodes<T>(Task<T> task, IEnumerable<DiscordApiErrorCodes> codes)
			where T : class
		{
			try
			{
				return await task;
			}
			catch (DiscordAPIException exception)
			{
				if (exception.ErrorCode != null && codes.Contains((DiscordApiErrorCodes) exception.ErrorCode))
				{
					return null;
				}

				throw;
			}
		}

		public static async Task<bool> ResolveAsBooleanOnErrorCodes<T>(Task<T> task,
			IEnumerable<DiscordApiErrorCodes> codes) where T : class
		{
			try
			{
				await task;
				return true;
			}
			catch (DiscordAPIException exception)
			{
				if (exception.ErrorCode != null && codes.Contains((DiscordApiErrorCodes) exception.ErrorCode))
				{
					return false;
				}

				throw;
			}
		}
	}
}

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Worker.Resources;

namespace Skyra.Worker.Extensions
{
	public static class CoreMessageExtensions
	{
		[ItemNotNull]
		public static async Task<CoreMessage> SendLocaleAsync([NotNull] this CoreMessage message, string key)
		{
			var language = await message.GetLanguageAsync();
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await message.SendAsync(content);
		}

		[ItemNotNull]
		public static async Task<CoreMessage> SendLocaleAsync([NotNull] this CoreMessage message, string key,
			[NotNull] params object?[] values)
		{
			var language = await message.GetLanguageAsync();
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await message.SendAsync(string.Format(content, values));
		}

		[ItemNotNull]
		public static async Task<CoreMessage> EditLocaleAsync([NotNull] this CoreMessage message, string key)
		{
			var language = await message.GetLanguageAsync();
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await message.EditAsync(content);
		}

		[ItemNotNull]
		public static async Task<CoreMessage> EditLocaleAsync([NotNull] this CoreMessage message, string key,
			[NotNull] params object?[] values)
		{
			var language = await message.GetLanguageAsync();
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await message.EditAsync(string.Format(content, values));
		}
	}
}

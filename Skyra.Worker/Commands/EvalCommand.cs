using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Services;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Utils;

// ReSharper disable All

namespace Skyra.Commands
{
	[Command(Inhibitors = new[] {"Developer"})]
	public sealed class EvalCommand : StructureBase
	{
		private readonly EvalService _eval;

		public EvalCommand(IClient client, EvalService eval) : base(client)
		{
			_eval = eval;
		}

		public async Task RunAsync([NotNull] CoreMessage message, string code)
		{
			var (threw, result, exception) = await ExecuteAsync(message, code);
			var output = Format(threw ? exception : result);
			if (output.Length > 1900)
			{
				try
				{
					var response =
						await Client.HttpClient.PostTextAsync<HastebinResponse>("https://hasteb.in/documents", output);
					await message.SendAsync($"Message too long, sent at <https://hasteb.in/{response.Key}.cs>");
				}
				catch
				{
					await message.SendAsync("Failed to send message via https://hasteb.in...");
				}
			}
			else
			{
				var codeBlock = Utilities.CodeBlock("js", output);
				await message.SendAsync(codeBlock);
			}
		}

		private async Task<(bool, object?, Exception?)> ExecuteAsync(CoreMessage message, string code)
		{
			var globals = new ScriptGlobals {Message = message, Client = Client};
			try
			{
				var result = await _eval.EvaluateAsync(code, globals);
				return (false, result, null);
			}
			catch (Exception exception)
			{
				return (true, null, exception);
			}
		}

		[NotNull]
		private string Format(object? result)
		{
			return result switch
			{
				string value => value,
				CompilationErrorException value => string.Join("\n", value.Diagnostics.Select(x => x.ToString())),
				Exception value => $"{value.GetType().Name}: {value.InnerException?.Message ?? value.Message}",
				_ => new InspectionFormatter(result).ToString()
			};
		}

		[UsedImplicitly]
		public sealed class ScriptGlobals
		{
			public CoreMessage Message { get; set; } = null!;
			public IClient Client { get; set; } = null!;
		}

		private sealed class HastebinResponse
		{
			[JsonProperty("key", Required = Required.Always)]
			public string Key { get; set; } = null!;
		}
	}
}

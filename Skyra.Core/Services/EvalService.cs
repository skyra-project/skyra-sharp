using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Spectacles.NET.Rest.APIError;
using Spectacles.NET.Types;

namespace Skyra.Core.Services
{
	public sealed class EvalService
	{
		private readonly ScriptOptions _options;

		public EvalService([NotNull] ScriptOptions options)
		{
			_options = options
				.WithImports("System", "System.Collections", "System.Collections.Generic")
				.WithReferences(typeof(Message).Assembly, typeof(DiscordAPIException).Assembly);
		}

		public async Task<object> EvaluateAsync<T>(string code, T globals)
		{
			var result = await CSharpScript.EvaluateAsync(code, globals: globals, options: _options);
			return result;
		}
	}
}

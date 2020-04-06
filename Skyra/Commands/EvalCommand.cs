using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Services;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Utils;

namespace Skyra.Commands
{
	[Command(Inhibitors = new[] {"Developer"})]
	public class EvalCommand : StructureBase
	{
		private readonly EvalService _eval;

		public EvalCommand(IClient client, EvalService eval) : base(client)
		{
			_eval = eval;
		}

		public async Task RunAsync(CoreMessage message, string code)
		{
			var globals = new ScriptGlobals {Message = message};
			try
			{
				var result = await _eval.EvaluateAsync(code, globals);

				// TODO(kyranet): hasteb.in upload
				// TODO(kyranet): Retrieve type
				// TODO(kyranet): CodeBlock utility
				await message.SendAsync($"```js\n{new InspectionFormatter(result, 1)}```");
			}
			catch (CompilationErrorException e)
			{
				await message.SendAsync(string.Join("\n", e.Diagnostics.Select(x => x.ToString())));
			}
		}

		public class ScriptGlobals
		{
			public CoreMessage? Message { get; set; }
		}
	}
}

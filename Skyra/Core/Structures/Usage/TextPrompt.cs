using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Spectacles.NET.Types;

namespace Skyra.Core.Structures.Usage
{
	public class TextPrompt
	{
		public Dictionary<string, string> Flags { get; }
		public string[] Arguments { get; }
		public object[] Parameters { get; private set; }
		public CommandUsageOverload Overload { get; private set; }
		private Message Message { get; }
		private CommandInfo Command { get; }

		private CommandUsage Usage { get; }
		// private bool QuotedStringSupport { get; }
		// private bool FlagSupport { get; }
		// private static readonly Regex FlagRegExp = new Regex(@"(?:--|—)(\w[\w-]+)(?:=(?:[""]((?:[^""\\]|\.)*)[""]|[']((?:[^'\\]|\.)*)[']|[“”]((?:[^“”\\]|\.)*)[“”]|[‘’]((?:[^‘’\\]|\.)*)[‘’]|([\w<>@​#&!-]+)))?", RegexOptions.Compiled);

		public TextPrompt(CommandInfo command, Message message, string content)
		{
			Message = message;
			Command = command;
			Usage = command.Usage;
			Arguments = GetArguments(content, command.Delimiter);
		}

		public async Task Run()
		{
			foreach (var overload in Usage.Overloads)
			{
				var error = await RunOverload(overload);
				if (error == null)
				{
					Overload = overload;
					return;
				}
				if (overload.Equals(Usage.Overloads.Last())) throw new ArgumentException(error);
			}
		}

		private async Task<string?> RunOverload(CommandUsageOverload overload)
		{
			Parameters = new object[overload.Arguments.Length + 1];
			Parameters[0] = Message;

			var inputIndex = 0;
			for (var i = 0; i < overload.Arguments.Length; i++)
			{
				var usageArgument = overload.Arguments[i];
				var inputArgument = Arguments[++inputIndex];
				try
				{
					Parameters[i + 1] =
						(usageArgument.Resolver.Method.Invoke(usageArgument.Resolver.Instance,
							new object?[] {Message, inputArgument}) as dynamic).Result as object;
					++inputIndex;
				}
				catch (TargetInvocationException exception)
				{
					if (usageArgument.Optional) Parameters[i] = null;
					return exception.InnerException?.Message ?? exception.Message;
				}
			}

			return null;
		}

		private static string[] GetArguments(string content, string? delimiter)
		{
			var arguments = string.IsNullOrEmpty(delimiter) ? new[] {content} : content.Split(delimiter);
			return arguments.Length == 1 && string.IsNullOrEmpty(arguments[0]) ? new string[0] : arguments;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Spectacles.NET.Types;

namespace Skyra.Core.Structures.Usage
{
	public class TextPrompt
	{
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

		public Dictionary<string, string> Flags { get; }
		public string[] Arguments { get; }
		public object[] Parameters { get; private set; }
		public CommandUsageOverload Overload { get; private set; }
		private Message Message { get; }
		private CommandInfo Command { get; }

		private CommandUsage Usage { get; }

		public async Task RunAsync()
		{
			for (var i = 0; i < Usage.Overloads.Length; i++)
			{
				var overload = Usage.Overloads[i];
				var error = await RunOverloadAsync(overload);
				if (error == null)
				{
					Overload = overload;
					return;
				}

				if (i == Usage.Overloads.Length - 1) throw new ArgumentException(error);
			}
		}

		private async Task<string?> RunOverloadAsync(CommandUsageOverload overload)
		{
			Parameters = new object[overload.Arguments.Length + 1];
			Parameters[0] = Message;

			var inputIndex = 1;
			for (var i = 0; i < overload.Arguments.Length; ++i)
			{
				var usageArgument = overload.Arguments[i];

				if (inputIndex >= Arguments.Length)
				{
					if (usageArgument.Optional) continue;
					return $"You must input a value for {usageArgument.Name}.";
				}

				var inputArgument = Arguments[inputIndex];
				try
				{
					Parameters[i + 1] =
						(usageArgument.Resolver.Method.Invoke(usageArgument.Resolver.Instance,
							new object?[] {Message, inputArgument}) as dynamic).Result as object;
					++inputIndex;
				}
				catch (TargetInvocationException exception)
				{
					if (usageArgument.Optional)
					{
						Parameters[i] = null;
					}
					else
					{
						return exception.InnerException?.Message ?? exception.Message;
					}
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

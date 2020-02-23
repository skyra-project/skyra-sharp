using System;
using System.Reflection;
using Spectacles.NET.Types;

namespace Skyra.Core.Structures.Usage
{
	public class CommandUsageParser
	{
		// private static readonly Regex FlagRegExp =
		// 	new Regex(
		// 		@"(?:--|—)(\w[\w-]+)(?:=(?:[""]((?:[^""\\]|\.)*)[""]|[']((?:[^'\\]|\.)*)[']|[“”]((?:[^“”\\]|\.)*)[“”]|[‘’]((?:[^‘’\\]|\.)*)[‘’]|([\w<>@​#&!-]+)))?",
		// 		RegexOptions.Compiled);

		public CommandUsageParser(CommandInfo command, Message message, string content)
		{
			Message = message;
			Usage = command.Usage;
			Overload = null;
			Parameters = new object?[0];
			Arguments = GetArguments(content, command.Delimiter);
		}

		// public Dictionary<string, string> Flags { get; }
		public string[] Arguments { get; }
		public object?[] Parameters { get; private set; }
		public CommandUsageOverload? Overload { get; private set; }
		// private bool QuotedStringSupport { get; }
		// private bool FlagSupport { get; }
		private Message Message { get; }

		private CommandUsage Usage { get; }

		public void Run()
		{
			for (var i = 0; i < Usage.Overloads.Length; i++)
			{
				var overload = Usage.Overloads[i];
				var error = RunOverloadAsync(overload);
				if (error == null)
				{
					Overload = overload;
					return;
				}

				if (i == Usage.Overloads.Length - 1) throw new ArgumentException(error);
			}
		}

		private string? RunOverloadAsync(CommandUsageOverload overload)
		{
			Parameters = new object?[overload.Arguments.Length + 1];
			Parameters[0] = Message;

			var inputIndex = 1;
			for (var i = 0; i < overload.Arguments.Length; ++i)
			{
				var usageArgument = overload.Arguments[i];

				if (inputIndex >= Arguments.Length)
				{
					if (!usageArgument.Optional) return $"You must input a value for {usageArgument.Name}.";
					Parameters[i + 1] = usageArgument.Default;
					continue;
				}

				var inputArgument = Arguments[inputIndex];
				try
				{
#pragma warning disable CS8600, CS8602
					Parameters[i + 1] =
						(usageArgument.Resolver.Method.Invoke(usageArgument.Resolver.Instance,
							new object[] {Message, inputArgument}) as dynamic).Result as object;
					++inputIndex;
#pragma warning restore CS8600, CS8602
				}
				catch (TargetInvocationException exception)
				{
					if (usageArgument.Optional)
					{
						Parameters[i] = usageArgument.Default;
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

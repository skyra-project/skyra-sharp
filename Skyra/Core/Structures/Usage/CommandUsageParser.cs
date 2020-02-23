// Copyright (c) 2017-2020 dirigeants. All rights reserved. MIT license.
// This is a derivative work adapted for .NET Core 3. Original source at:
// https://github.com/dirigeants/klasa/blob/master/src/lib/usage/TextPrompt.js

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Skyra.Core.Utils;
using Spectacles.NET.Types;

namespace Skyra.Core.Structures.Usage
{
	public class CommandUsageParser
	{
		private static readonly Regex FlagRegExp =
			new Regex(
				@"(?:--|—)(\w[\w-]+)(?:=(?:[""]((?:[^""\\]|\.)*)[""]|[']((?:[^'\\]|\.)*)[']|[“”]((?:[^“”\\]|\.)*)[“”]|[‘’]((?:[^‘’\\]|\.)*)[‘’]|([\w<>@​#&!-]+)))?",
				RegexOptions.Compiled);

		private static readonly string[] Quotes = {"\"", "'", "“”", "‘’"};

		public CommandUsageParser(CommandInfo command, Message message, string content)
		{
			Message = message;
			Usage = command.Usage;

			var (c, f) = command.FlagSupport
				? GetFlags(content, command.Delimiter)
				: (content, new Dictionary<string, string>());
			Flags = f;
			Arguments = command.QuotedStringSupport
				? GetQuotedStringArgs(c.Trim(), command.Delimiter)
				: GetArguments(c.Trim(), command.Delimiter);

			Overload = null;
			Parameters = new object?[0];
		}

		public Dictionary<string, string> Flags { get; }
		public string[] Arguments { get; }
		public object?[] Parameters { get; private set; }
		public CommandUsageOverload? Overload { get; private set; }
		private Message Message { get; }

		private CommandUsage Usage { get; }

		private static Dictionary<string, Regex> Delimiters { get; } = new Dictionary<string, Regex>();

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

			var inputIndex = 0;
			for (var i = 0; i < overload.Arguments.Length; ++i)
			{
				var skip = false;
				var usageArgument = overload.Arguments[i];

				if (!Flags.TryGetValue(usageArgument.Name, out var inputArgument))
				{
					if (inputIndex >= Arguments.Length)
					{
						if (!usageArgument.Optional) return $"You must input a value for {usageArgument.Name}.";
						Parameters[i + 1] = usageArgument.Default;
						continue;
					}

					inputArgument = Arguments[inputIndex];
					skip = true;
				}

				try
				{
#pragma warning disable CS8600, CS8602
					Parameters[i + 1] =
						(usageArgument.Resolver.Method.Invoke(usageArgument.Resolver.Instance,
							new object[] {Message, usageArgument, inputArgument}) as dynamic).Result as object;
					if (skip) ++inputIndex;
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

		private static (string, Dictionary<string, string>) GetFlags(string content, string delimiter)
		{
			var flags = new Dictionary<string, string>();
			content = content.Replace(FlagRegExp, captures =>
			{
				flags.Add(captures[1].Value,
					captures.Skip(2).FirstOrDefault(x => !string.IsNullOrEmpty(x.Value))?.Value ?? captures[1].Value);
				return "";
			});

			if (string.IsNullOrEmpty(delimiter)) return (content, flags);

			if (!Delimiters.TryGetValue(delimiter, out var reg))
			{
				// TODO(kyranet): Add RegExpEsc.
				reg = new Regex($"({delimiter})(?:{delimiter})+", RegexOptions.Compiled);
				Delimiters.Add(delimiter, reg);
			}

			content = content.Replace(reg, captures => captures[1].Value);

			return (content, flags);
		}

		private static string[] GetQuotedStringArgs(string content, string delimiter)
		{
			if (string.IsNullOrEmpty(content)) return new[] {content};

			var args = new List<string>();
			var current = new StringBuilder();

			for (var i = 0; i < content.Length; ++i)
			{
				if (content.Substring(i, delimiter.Length) == delimiter)
				{
					i += delimiter.Length - 1;
					continue;
				}

				var quote = Quotes.FirstOrDefault(qt => qt.Contains(content[i]));
				if (string.IsNullOrEmpty(quote))
				{
					current.Append(content[i]);
					while (i + 1 < content.Length && content.Substring(i + 1, delimiter.Length) != delimiter)
					{
						current.Append(content[++i]);
					}
				}
				else
				{
					var qts = quote.AsSpan();
					while (i + 1 < content.Length && (content[i] == '\\' || !qts.Contains(content[i + 1])))
					{
						current.Append(content[++i]);
					}

					++i;
				}

				args.Add(current.ToString());
				current.Clear();
			}

			return args.Count == 1 && string.IsNullOrEmpty(args[0]) ? new string[0] : args.ToArray();
		}
	}
}

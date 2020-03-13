// Copyright (c) 2017-2020 dirigeants. All rights reserved. MIT license.
// This is a derivative work adapted for .NET Core 3. Original source at:
// https://github.com/dirigeants/klasa/blob/master/src/lib/usage/TextPrompt.js

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Skyra.Core.Cache.Models;
using Skyra.Core.Utils;

namespace Skyra.Core.Structures.Usage
{
	internal sealed class CommandUsageParser
	{
		private static readonly Regex FlagRegExp =
			new Regex(
				@"(?:--|—)(\w[\w-]+)(?:=(?:[""]((?:[^""\\]|\.)*)[""]|[']((?:[^'\\]|\.)*)[']|[“”]((?:[^“”\\]|\.)*)[“”]|[‘’]((?:[^‘’\\]|\.)*)[‘’]|([\w<>@​#&!-]+)))?",
				RegexOptions.Compiled);

		private static readonly string[] Quotes = {"\"", "'", "“”", "‘’"};

		public CommandUsageParser(CommandInfo command, CoreMessage message, string content)
		{
			Message = message;
			Command = command;
			Usage = command.Usage;

			var (c, f) = command.FlagSupport
				? GetFlags(content, command.Delimiter)
				: (content, new Dictionary<string, string>());
			Flags = f;
			Arguments = command.QuotedStringSupport
				? GetQuotedStringArgs(c.Trim(), command.Delimiter)
				: GetArguments(c.Trim(), command.Delimiter);

			Overload = null;
			Argument = null;
			Parameters = new object?[0];
			ParameterPosition = 0;
			ArgumentPosition = 0;
		}

		internal Dictionary<string, string> Flags { get; }
		internal string[] Arguments { get; }
		public object?[] Parameters { get; private set; }
		internal CommandInfo Command { get; }
		internal CommandUsage Usage { get; }
		internal CommandUsageOverload? Overload { get; private set; }
		internal CommandUsageOverloadArgument? Argument { get; private set; }
		private CoreMessage Message { get; }
		private int ParameterPosition { get; set; }
		private int ArgumentPosition { get; set; }

		private static Dictionary<string, Regex> Delimiters { get; } = new Dictionary<string, Regex>();

		public void Run()
		{
			for (var i = 0; i < Usage.Overloads.Length; ++i)
			{
				var overload = Usage.Overloads[i];
				try
				{
					RunOverloadAsync(overload);
					Overload = overload;
					return;
				}
				catch
				{
					if (i == Usage.Overloads.Length - 1) throw;
				}
			}
		}

		private object? Resolve(string value)
		{
			try
			{
#pragma warning disable 8600
				return ((dynamic) Argument!.Resolver.Method.Invoke(Argument!.Resolver.Instance,
					new object[] {Message, Argument!, value}))?.Result;
#pragma warning restore 8600
			}
			catch (TargetInvocationException exception)
			{
				throw exception.InnerException ?? exception;
			}
		}

		private object? ResolveNextArgument()
		{
			if (Flags.TryGetValue(Argument!.Name, out var argument))
			{
				try
				{
					return Resolve(argument);
				}
				catch
				{
					if (Argument!.Optional) return Argument!.Default!;
					throw;
				}
			}

			if (ParameterPosition == Arguments.Length)
			{
				if (Argument!.Optional) return Argument!.Default;
				throw new ArgumentException($"You must input a value for {Argument!.Name}");
			}

			try
			{
				object? resolved;
				if (Argument!.Rest)
				{
					resolved = Resolve(string.Join(Command.Delimiter, Arguments.Skip(ParameterPosition).ToArray()));
					ParameterPosition = Arguments.Length - 1;
				}
				else
				{
					resolved = Resolve(Arguments[ParameterPosition]);
					++ParameterPosition;
				}

				return resolved;
			}
			catch
			{
				if (Argument!.Optional) return Argument!.Default;
				throw;
			}
		}

		private IEnumerable ResolveNextArgumentsFromFlags(string argument)
		{
			if (!string.IsNullOrEmpty(Command.Delimiter))
			{
				return Cast(Argument!.Type, argument.Split(Command.Delimiter).Select(Resolve).ToArray());
			}

			var value = Convert.ChangeType(Resolve(argument), Argument!.Type);
			return new[] {value};
		}

		private static IEnumerable Cast(Type type, object?[] values)
		{
			var array = Array.CreateInstance(type, values.Length);
			Array.Copy(values, array, values.Length);
			return array;
		}

		private IEnumerable ResolveNextArgumentsFromArguments()
		{
			var values = new List<object?>();
			for (var i = 0; i < Argument!.MaximumValues; ++i)
			{
				try
				{
					if (ParameterPosition == Arguments.Length && values.Count < Argument!.MinimumValues)
					{
						throw new ArgumentException($"There are not enough values for {Argument!.Name}");
					}

					var argument = Arguments[ParameterPosition];
					var resolved = Resolve(argument);
					values.Add(resolved);
					++ParameterPosition;
				}
				catch
				{
					if (values.Count == 0 && Argument!.Optional)
					{
						return (IEnumerable) Argument!.Default!;
					}

					if (values.Count < Argument!.MinimumValues)
					{
						throw new ArgumentException($"There are not enough values for {Argument!.Name}");
					}

					break;
				}
			}

			return Cast(Argument!.Type, values.ToArray());
		}

		private IEnumerable ResolveNextArguments()
		{
			return Flags.TryGetValue(Argument!.Name, out var argument)
				? ResolveNextArgumentsFromFlags(argument)
				: ResolveNextArgumentsFromArguments();
		}

		private void RunOverloadAsync(CommandUsageOverload overload)
		{
			Parameters = new object?[overload.Arguments.Length + 1];
			Parameters[0] = Message;
			ParameterPosition = 0;

			for (ArgumentPosition = 0; ArgumentPosition < overload.Arguments.Length; ++ArgumentPosition)
			{
				Argument = overload.Arguments[ArgumentPosition];
				Parameters[ArgumentPosition + 1] = Argument.Repeating
					? ResolveNextArguments()
					: ResolveNextArgument();
			}
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
				var escaped = delimiter.EscapeRegexPatterns();
				reg = new Regex($"({escaped})(?:{escaped})+", RegexOptions.Compiled);
				Delimiters.Add(delimiter, reg);
			}

			content = content.Replace(reg, captures => captures[1].Value);

			return (content, flags);
		}

		private static string[] GetQuotedStringArgs(string content, string delimiter)
		{
			if (string.IsNullOrEmpty(delimiter)) return new[] {content};

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
				args.Add(GetQuotedStringArg(content, delimiter, quote, ref current, ref i));
			}

			return args.Count == 1 && string.IsNullOrEmpty(args[0]) ? new string[0] : args.ToArray();
		}

		private static string GetQuotedStringArg(string content, string delimiter, string quote,
			ref StringBuilder current, ref int i)
		{
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

			var output = current.ToString();
			current.Clear();
			return output;
		}
	}
}

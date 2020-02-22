using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Skyra.Commands.Arguments;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	public class CommandHandler
	{
		private Dictionary<string, CommandInfo> _commands;
		private Dictionary<Type, ArgumentInfo> _resolvers;

		public void Load(Client client)
		{
			_commands = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<CommandAttribute>() != null)
				.Select(type => Activator.CreateInstance(type, client))
				.Select(ToCommandInfo).ToDictionary(x => x.Name, x => x);

			_resolvers = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<ResolverAttribute>() != null)
				.Select(type => Activator.CreateInstance(type))
				.Select(ToArgumentInfo)
				.ToDictionary(x => x.Type, x => x);
		}

		public async Task Run(Message message)
		{
			const string prefix = "t!";
			var prefixLess = message.Content.Replace(prefix, "");

			string commandName;

			commandName = prefixLess.Contains(" ")
				? prefixLess.Substring(0, prefixLess.IndexOf(" "))
				: prefixLess;

			var command = _commands[commandName.ToLower()];
			var args = new object[command.Arguments.Length + 1];
			args[0] = message;
			if (command.Arguments.Count() > 0)
			{
				var replaced = prefixLess.Replace(commandName, "");
				var trimmed = replaced.Trim();
				var split = trimmed.Split(command.Delimiter);

				for (var i = 0; i < command.Arguments.Count(); i++)
				{
					var resolver = _resolvers[command.Arguments[i]];
					var resolved =
						(object) ((dynamic) resolver.Method.Invoke(resolver.Instance, new object[] {message, split[i]}))
						.Result;
					args[i + 1] = resolved;
				}
			}
			try
			{
				await (Task) command.Method.Invoke(command.Instance, args);
			}
			catch (Exception exception)
			{
				Console.Error.WriteLine($"[COMMANDS]: {exception.Message}\n{exception.StackTrace}");
			}
		}

		public ArgumentInfo ToArgumentInfo(object argument)
		{
			var attribute = argument.GetType().GetCustomAttribute<ResolverAttribute>();

			return new ArgumentInfo
			{
				Instance = argument,
				Method = argument.GetType().GetMethod("ResolveAsync"),
				Type = attribute.Type
			};
		}

		public CommandInfo ToCommandInfo(object command)
		{
			var t = command.GetType();
			var methodInfo = t.GetMethod("RunAsync", BindingFlags.Public | BindingFlags.Instance);

			var commandInfo = t.GetCustomAttribute<CommandAttribute>();

			return new CommandInfo
			{
				Delimiter = commandInfo.Delimiter,
				Instance = command,
				Method = methodInfo,
				Arguments = methodInfo?.GetParameters().Select(x => x.ParameterType).Skip(1).ToArray(),
				Name = commandInfo.Name ?? command.GetType().Name.Replace("Command", "").ToLower()
			};
		}
	}
}

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
		private Dictionary<Type, MethodInfo> _resolvers;

		public void Load(Client client)
		{
			_commands = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<CommandAttribute>() != null)
				.Select(type => Activator.CreateInstance(type, client))
				.Select(ToCommandInfo).ToDictionary(x => x.Name, x => x);

			_resolvers = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.IsAssignableFrom(typeof(IArgumentResolver<>)))
				.Select(x => x.GetMethod("Resolve"))
				.ToDictionary(x => x?.ReturnType, x => x);
		}

		public async Task Run(Message message)
		{
			const string prefix = "t!";
			var prefixLess = message.Content.Replace(prefix, "");

			string commandName;

			commandName = prefixLess.Contains(" ")
				? prefixLess.Substring(0, prefixLess.IndexOf(" ", StringComparison.Ordinal))
				: prefixLess;

			var command = _commands[commandName.ToLower()];
			// TODO(Tylertron1998): add argument resolving
			try
			{
				await (Task) command.Method.Invoke(command.Instance, new[] {message});
			}
			catch (Exception exception)
			{
				Console.Error.WriteLine($"[COMMANDS]: {exception.Message}\n{exception.StackTrace}");
			}
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Skyra.Commands.Arguments;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	public class CommandHandler
	{
		private Dictionary<Type, MethodInfo> _resolvers;
		private Dictionary<string, CommandInfo> _commands;

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
				.ToDictionary(x => x.ReturnType, x => x);
		}

		public async Task Run(Message message)
		{
			const string prefix = "t!";
			var prefixLess = message.Content.Replace(prefix, "");

			string commandName;

			if (prefixLess.Contains(" "))
			{
				commandName = prefixLess.Substring(0, prefixLess.IndexOf(" "));
			}
			else
			{
				commandName = prefixLess;
			}

			var command = _commands[commandName.ToLower()];
			// TODO(Tylertron1998): add argument resolving
			await (Task) command.Method.Invoke(command.Instance, new[] {message});
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
				Arguments = methodInfo.GetParameters().Select(x => x.ParameterType).Skip(1).ToArray(),
				Name = commandInfo.Name ?? command.GetType().Name.Replace("Command", "").ToLower()
			};
		}
	}
}

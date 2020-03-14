using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Base;
using Skyra.Core.Structures.Usage;
using EventInfo = Skyra.Core.Structures.EventInfo;

namespace Skyra.Core
{
	internal static class Loader
	{
		public static ImmutableDictionary<string, CultureInfo> LoadCultures([NotNull] IEnumerable<string> cultures)
		{
			return cultures.ToImmutableDictionary(x => x, x => new CultureInfo(x));
		}

		public static Dictionary<string, InhibitorInfo> LoadInhibitors([NotNull] Client client)
		{
			return Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<InhibitorAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(client.ServiceProvider, type))
				.Select(ToInhibitorInfo).ToDictionary(x => x.Name, x => x);
		}

		public static Dictionary<string, CommandInfo> LoadCommands([NotNull] Client client)
		{
			return Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<CommandAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(client.ServiceProvider, type))
				.Select(x => ToCommandInfo(client, x)).ToDictionary(x => x.Name, x => x);
		}

		public static Dictionary<string, EventInfo> LoadEvents([NotNull] Client client)
		{
			return Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<EventAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(client.ServiceProvider, type))
				.Select(ToEventInfo).ToDictionary(x => x.Name, x => x);
		}

		public static Dictionary<string, MonitorInfo> LoadMonitors([NotNull] Client client)
		{
			return Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<MonitorAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(client.ServiceProvider, type))
				.Select(ToMonitorInfo).ToDictionary(x => x.Name, x => x);
		}

		public static Dictionary<Type, ResolverInfo> LoadResolvers([NotNull] Client client)
		{
			return Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<ResolverAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(client.ServiceProvider, type))
				.Select(ToArgumentInfo)
				.ToDictionary(x => x.Type, x => x);
		}

		private static ResolverInfo ToArgumentInfo(object argument)
		{
			var attribute = argument.GetType().GetCustomAttribute<ResolverAttribute>()!;

			return new ResolverInfo
			{
				Instance = argument,
				Method = argument.GetType().GetMethod("ResolveAsync")!,
				Type = attribute.Type,
				DisplayName = attribute.DisplayName
			};
		}

		private static InhibitorInfo ToInhibitorInfo(object inhibitor)
		{
			var attribute = inhibitor.GetType().GetCustomAttribute<InhibitorAttribute>()!;

			return new InhibitorInfo
			{
				Name = attribute.Name ?? inhibitor.GetType().Name.Replace("Inhibitor", ""),
				Instance = (IInhibitor) inhibitor
			};
		}

		private static EventInfo ToEventInfo(object @event)
		{
			var attribute = @event.GetType().GetCustomAttribute<EventAttribute>()!;

			return new EventInfo
			{
				Instance = @event,
				Name = attribute.Name ?? @event.GetType().Name
			};
		}

		private static MonitorInfo ToMonitorInfo(object monitor)
		{
			var attribute = monitor.GetType().GetCustomAttribute<MonitorAttribute>()!;
			return new MonitorInfo
			{
				Instance = (IMonitor) monitor,
				Name = attribute.Name ?? monitor.GetType().Name,
				AllowedTypes = attribute.AllowedTypes,
				IgnoreBots = attribute.IgnoreBots,
				IgnoreEdits = attribute.IgnoreEdits,
				IgnoreOthers = attribute.IgnoreOthers,
				IgnoreSelf = attribute.IgnoreSelf,
				IgnoreWebhooks = attribute.IgnoreWebhooks
			};
		}

		private static CommandInfo ToCommandInfo(Client client, object command)
		{
			var t = command.GetType();
			var commandInfo = t.GetCustomAttribute<CommandAttribute>()!;

			return new CommandInfo
			{
				Delimiter = commandInfo.Delimiter,
				Instance = command,
				Name = commandInfo.Name ?? command.GetType().Name.Replace("Command", "").ToLower(),
				Usage = new CommandUsage(client, command),
				FlagSupport = commandInfo.FlagSupport,
				QuotedStringSupport = commandInfo.QuotedStringSupport,
				Inhibitors = commandInfo.Inhibitors.Select(v => client.Inhibitors[v].Instance).ToArray()
			};
		}
	}
}

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
	internal sealed class Loader
	{
		public Assembly Assembly { get; }

		public Loader(Assembly? assembly = null)
		{
			Assembly = assembly ?? Assembly.GetExecutingAssembly();
		}
		public ImmutableDictionary<string, CultureInfo> LoadCultures([NotNull] IEnumerable<string> cultures)
		{
			return cultures.ToImmutableDictionary(x => x, x => new CultureInfo(x));
		}

		public Dictionary<string, InhibitorInfo> LoadInhibitors([NotNull] IClient client)
		{
			return Assembly
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<InhibitorAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(client.ServiceProvider, type) as IInhibitor)
				.Select(ToInhibitorInfo).ToDictionary(x => x.Name, x => x);
		}

		public Dictionary<string, CommandInfo> LoadCommands([NotNull] IClient client)
		{
			return Assembly
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<CommandAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(client.ServiceProvider, type))
				.Select(x => ToCommandInfo(client, x)).ToDictionary(x => x.Name, x => x);
		}

		public Dictionary<string, EventInfo> LoadEvents([NotNull] IClient client)
		{
			return Assembly
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<EventAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(client.ServiceProvider, type))
				.Select(ToEventInfo).ToDictionary(x => x.Name, x => x);
		}

		public Dictionary<string, MonitorInfo> LoadMonitors([NotNull] IClient client)
		{
			return Assembly
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<MonitorAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(client.ServiceProvider, type) as IMonitor)
				.Select(ToMonitorInfo).ToDictionary(x => x.Name, x => x);
		}

		public Dictionary<Type, ResolverInfo> LoadResolvers([NotNull] IClient client)
		{
			return Assembly
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<ResolverAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(client.ServiceProvider, type))
				.Select(ToArgumentInfo)
				.ToDictionary(x => x.Type, x => x);
		}

		private ResolverInfo ToArgumentInfo(object argument)
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

		private InhibitorInfo ToInhibitorInfo(IInhibitor inhibitor)
		{
			var attribute = inhibitor.GetType().GetCustomAttribute<InhibitorAttribute>()!;

			return new InhibitorInfo
			{
				Name = attribute.Name ?? inhibitor.GetType().Name.Replace("Inhibitor", ""),
				Instance = inhibitor
			};
		}

		private EventInfo ToEventInfo(object @event)
		{
			var attribute = @event.GetType().GetCustomAttribute<EventAttribute>()!;

			return new EventInfo
			{
				Instance = @event,
				Name = attribute.Name ?? @event.GetType().Name
			};
		}

		private MonitorInfo ToMonitorInfo(IMonitor monitor)
		{
			var attribute = monitor.GetType().GetCustomAttribute<MonitorAttribute>()!;
			return new MonitorInfo
			{
				Instance = monitor,
				Name = attribute.Name ?? monitor.GetType().Name,
				AllowedTypes = attribute.AllowedTypes,
				IgnoreBots = attribute.IgnoreBots,
				IgnoreEdits = attribute.IgnoreEdits,
				IgnoreOthers = attribute.IgnoreOthers,
				IgnoreSelf = attribute.IgnoreSelf,
				IgnoreWebhooks = attribute.IgnoreWebhooks
			};
		}

		private CommandInfo ToCommandInfo(IClient client, object command)
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

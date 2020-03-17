using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Skyra.Core.Cache;
using Skyra.Core.Structures;
using Spectacles.NET.Rest;

namespace Skyra.Core
{
	public interface IClient
	{
		Dictionary<string, InhibitorInfo> Inhibitors { get; }
		Dictionary<string, CommandInfo> Commands { get; }
		Dictionary<string, EventInfo> Events { get; }
		Dictionary<string, MonitorInfo> Monitors { get; }
		Dictionary<Type, ResolverInfo> Resolvers { get; }
		ServiceProvider ServiceProvider { get; }
		ulong? Id { get; set; }
		ulong[] Owners { get; set; }
		RestClient Rest { get; }
		ImmutableDictionary<string, CultureInfo> Cultures { get; }
		EventHandler EventHandler { get; }
		CacheClient Cache { get; }
		Logger Logger { get; }
		Task ConnectAsync();
	}
}

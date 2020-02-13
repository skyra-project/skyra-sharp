using System;
using System.Threading.Tasks;
using Skyra.Gateway.Core;
using Spectacles.NET.Gateway;
using Spectacles.NET.Types;

namespace Skyra.Gateway
{
	public static class Program
	{
		public static async Task Main()
		{
			var token = Environment.GetEnvironmentVariable("DISCORD_TOKEN")
			            ?? throw new ArgumentNullException("DISCORD_TOKEN"); // TODO: replace these with EnvironmentVariableNullException
			var brokerName = Environment.GetEnvironmentVariable("BROKER_NAME")
			                 ?? throw new ArgumentNullException("BROKER_NAME");
			var brokerUrl = Environment.GetEnvironmentVariable("BROKER_URL")
			                ?? throw new ArgumentNullException("BROKER_URL");
			var shardCount = Environment.GetEnvironmentVariable("DISCORD_SHARD_COUNT") ?? "";


			var identifyOptions = new IdentifyOptions
			{
				GuildSubscriptions = true,
				LargeThreshold = 250,
				Presence = new UpdateStatusDispatch {Status = "Skyra, help"}
			};

			var gateway = new GatewayHandler(token,
				brokerName,
				new Uri(brokerUrl),
				identifyOptions,
				int.Parse(shardCount));

			await gateway.ConnectAsync();
		}
	}
}

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
			// TODO: Replace this with EnvironmentVariableNullException
			var token = Environment.GetEnvironmentVariable("DISCORD_TOKEN") ??
			            throw new ArgumentNullException("DISCORD_TOKEN");
			var brokerName = Environment.GetEnvironmentVariable("BROKER_NAME") ?? "skyra";
			var brokerUrl = Environment.GetEnvironmentVariable("BROKER_URL") ?? "amqp://localhost:5672";
			var shardCount = Environment.GetEnvironmentVariable("DISCORD_SHARD_COUNT") ?? "1";

			var identifyOptions = new IdentifyOptions
			{
				GuildSubscriptions = true,
				LargeThreshold = 250,
				Presence = new UpdateStatusDispatch
					{Game = new Activity {Name = "Skyra.Worker, help", Type = ActivityType.LISTENING}}
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

using System;
using System.Threading.Tasks;
using Skyra.Core.Models;

namespace Skyra.Worker
{
	public static class Program
	{
		public static async Task Main()
		{
			await StartAsync();
		}

		private static async Task StartAsync()
		{
			var client = new Client(new ClientOptions(
				// ReSharper disable once NotResolvedInText
				Environment.GetEnvironmentVariable("DISCORD_TOKEN") ?? throw new ArgumentNullException("DISCORD_TOKEN"),
				Environment.GetEnvironmentVariable("BROKER_NAME") ?? "skyra",
				Environment.GetEnvironmentVariable("BROKER_URL") ?? "amqp://localhost:5672",
				Environment.GetEnvironmentVariable("REDIS_PREFIX") ?? "skyra",
				Environment.GetEnvironmentVariable("REDIS_URL") ?? "localhost",
				Environment.GetEnvironmentVariable("OWNERS"))
			);

			await client.ConnectAsync();
		}
	}
}

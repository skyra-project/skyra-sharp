using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Models;
using Skyra.Events;
using Skyra.Monitors;

namespace Skyra
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
				Environment.GetEnvironmentVariable("REDIS_URL") ?? "localhost")
			);

			PopulateCache(client);
			await client.ConnectAsync();
		}

		private static void PopulateCache(Client client)
		{
			client.Events
				.Insert(new EventGuildCreate(client))
				.Insert(new EventGuildUpdate(client))
				.Insert(new EventMessageCreate(client))
				.Insert(new EventMessageEdit(client))
				.Insert(new EventReady(client));

			client.Monitors
				.Insert(new CommandHandler(client))
				.Insert(new SocialCounter(client));
		}
	}
}

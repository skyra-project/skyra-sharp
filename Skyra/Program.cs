using System;
using System.Threading.Tasks;
using Skyra.Events;
using Skyra.Models;
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
				Environment.GetEnvironmentVariable("BROKER_NAME") ?? "skyra",
				Environment.GetEnvironmentVariable("BROKER_URL") ?? throw new ArgumentNullException("BROKER_URL"),
				Environment.GetEnvironmentVariable("REDIS_PREFIX") ?? "skyra",
				Environment.GetEnvironmentVariable("REDIS_URL") ?? throw new ArgumentNullException("REDIS_URL"))
			);

			PopulateCache(client);
			await client.ConnectAsync();
		}

		private static void PopulateCache(Client client)
		{
			client.Events
				.Insert(new EventMessageCreate(client))
				.Insert(new EventMessageEdit(client))
				.Insert(new EventReady(client));

			client.Monitors
				.Insert(new CommandHandler(client))
				.Insert(new SocialCounter(client));
		}
	}
}

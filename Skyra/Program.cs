using System;
using System.Threading.Tasks;
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
			var brokerName = Environment.GetEnvironmentVariable("BROKER_NAME")
			                 ?? throw new ArgumentNullException("BROKER_NAME");
			var brokerUrl = Environment.GetEnvironmentVariable("BROKER_URL")
			                ?? throw new ArgumentNullException("BROKER_URL");

			var client = new Client(brokerName, new Uri(brokerUrl));

			PopulateCache(client);
			await client.ConnectAsync();
		}

		private static void PopulateCache(Client client)
		{
			client.Events
				.Insert(new EventMessage(client))
				.Insert(new EventReady(client));

			client.Monitors
				.Insert(new CommandHandler(client))
				.Insert(new SocialCounter(client));
		}
	}
}

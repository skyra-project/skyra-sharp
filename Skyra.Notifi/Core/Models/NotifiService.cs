using System;
using System.Threading.Tasks;
using Skyra.Notifi.Core.Database;
using Spectacles.NET.Broker.Amqp;

namespace Skyra.Notifi.Core.Models
{
	public class NotifiService
	{
		private readonly Uri _brokerUrl;

		public NotifiService()
		{
			var brokerName = Environment.GetEnvironmentVariable("BROKER_NAME") ?? "skyra";
			var brokerUrl = Environment.GetEnvironmentVariable("BROKER_URL") ?? "amqp://localhost:5672";
			TwitchToken = Environment.GetEnvironmentVariable("TWITCH_TOKEN") ??
			              throw new ArgumentNullException("TWITCH_TOKEN");

			Broker = new AmqpBroker(brokerName);
			_brokerUrl = new Uri(brokerUrl);
		}

		public string TwitchToken { get; }
		public AmqpBroker Broker { get; }
		public NotifiDatabaseContext Db { get; set; }

		public async Task StartAsync()
		{
			await Broker.ConnectAsync(_brokerUrl);
			Db = new NotifiDatabaseContext();
		}
	}
}

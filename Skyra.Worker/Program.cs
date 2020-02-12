using System;
using System.Threading.Tasks;
using Skyra.Framework;

namespace Skyra
{
	public static class Program
	{
		public static void Main()
			=> Start().GetAwaiter().GetResult();

		private static async Task Start()
		{
			var brokerName = Environment.GetEnvironmentVariable("BROKER_NAME");
			var brokerUrl = Environment.GetEnvironmentVariable("BROKER_URL");

			if (brokerName == null || brokerUrl == null)
				throw new SystemException("Missing core arguments");

			var client = new Client(brokerName, new Uri(brokerUrl));
			client.OnReady += (_, args) => { Console.WriteLine("Got ready!"); };
			client.OnMessageCreate += (_, args) => { Console.WriteLine($"Got Message! {args.Data.Id} Content: {args.Data.Content}"); };
			await client.ConnectAsync();
		}
	}
}

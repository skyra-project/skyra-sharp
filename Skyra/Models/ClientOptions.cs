using System;

namespace Skyra.Models
{
	public readonly ref struct ClientOptions
	{
		public string BrokerName { get; }
		public string BrokerUri { get; }
		public string RedisPrefix { get; }
		public string RedisUri { get; }

		public ClientOptions(string brokerName, string brokerUri, string redisPrefix, string redisUri)
		{
			BrokerName = brokerName;
			BrokerUri = brokerUri;
			RedisPrefix = redisPrefix;
			RedisUri = redisUri;
		}
	}
}

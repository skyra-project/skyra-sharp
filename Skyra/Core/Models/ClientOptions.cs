namespace Skyra.Core.Models
{
	public readonly ref struct ClientOptions
	{
		public string Token { get; }
		public string BrokerName { get; }
		public string BrokerUri { get; }
		public string RedisPrefix { get; }
		public string RedisUri { get; }

		public ClientOptions(string token, string brokerName, string brokerUri, string redisPrefix, string redisUri)
		{
			Token = token;
			BrokerName = brokerName;
			BrokerUri = brokerUri;
			RedisPrefix = redisPrefix;
			RedisUri = redisUri;
		}
	}
}

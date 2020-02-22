using System;
using System.Reflection;

namespace Skyra.Core.Structures.Usage
{
	public struct CommandUsageOverloadArgument
	{
		private Client Client { get; }
		public ArgumentInfo Resolver { get; }
		public string Name { get; }
		public Type Type { get; }
		public bool Optional { get; }


		internal CommandUsageOverloadArgument(Client client, ParameterInfo parameterInfo)
		{
			var underlyingType = Nullable.GetUnderlyingType(parameterInfo.GetType());
			Client = client;
			Name = parameterInfo.Name;
			Type = underlyingType ?? parameterInfo.ParameterType;
			Optional = underlyingType != null;
			Resolver = Client.Resolvers[Type];
		}
	}
}

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
		public object? Default { get; }

		public override string ToString()
		{
			var formatted = Type.IsEnum
				? string.Join("|", Type.GetEnumNames()).ToLower()
				: $"{Name}:{Resolver.Displayname}";
			return Optional ? $"[{formatted}]" : $"<{formatted}>";
		}

		internal CommandUsageOverloadArgument(Client client, ParameterInfo parameterInfo)
		{
			var underlyingType = Nullable.GetUnderlyingType(parameterInfo.GetType());
			Client = client;
			Name = parameterInfo.Name;
			Type = underlyingType ?? parameterInfo.ParameterType;
			Optional = underlyingType != null || parameterInfo.HasDefaultValue;
			Resolver = Client.Resolvers[Type];
			Default = parameterInfo.DefaultValue;
		}
	}
}

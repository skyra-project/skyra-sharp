using System;
using System.Reflection;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Core.Structures.Usage
{
	public sealed class CommandUsageOverloadArgument
	{
		internal CommandUsageOverloadArgument(IClient client, ParameterInfo parameterInfo)
		{
			ArgumentType = parameterInfo.ParameterType;
			var innerType = ArgumentType.GetElementType() ?? ArgumentType;
			var underlyingType = Nullable.GetUnderlyingType(innerType);
			Client = client;
			Name = parameterInfo.Name!;
			Type = underlyingType ?? innerType;
			Optional = underlyingType != null || parameterInfo.HasDefaultValue;
			Resolver = Client.Resolvers[Type];
			Default = parameterInfo.DefaultValue;
			Repeating = ArgumentType.IsArray;

			var attribute = parameterInfo.GetCustomAttribute<ArgumentAttribute>();
			Rest = attribute?.Rest ?? false;
			Minimum = attribute?.Minimum ?? int.MinValue;
			Maximum = attribute?.Maximum ?? int.MaxValue;
			MinimumValues = attribute?.MinimumValues ?? CalculatedMinimumValues;
			MaximumValues = attribute?.MaximumValues ?? uint.MaxValue;
		}

		private IClient Client { get; }
		public ResolverInfo Resolver { get; }
		public string Name { get; }
		public Type ArgumentType { get; }
		public Type Type { get; }
		public bool Optional { get; }
		public bool Repeating { get; }
		public object? Default { get; }
		public bool Rest { get; }
		public int Minimum { get; }
		public int Maximum { get; }
		public uint MinimumValues { get; }
		public uint MaximumValues { get; }

		private uint CalculatedMinimumValues => Repeating && !Optional ? 1U : uint.MinValue;

		private string FormattedInternalUsage
		{
			get
			{
				var formatted = Type.IsEnum
					? Repeating ? $"{ArrayRangeString}({string.Join("|", Type.GetEnumNames()).ToLower()})" :
					$"{string.Join("|", Type.GetEnumNames()).ToLower()}"
					: $"{Name}:{ArrayRangeString}{Resolver.DisplayName}";
				var rest = Rest ? " [...]" : "";
				return $"{formatted}{RangeString}{rest}";
			}
		}

		private string RangeString
		{
			get
			{
				var hasMinimum = Minimum != int.MinValue;
				var hasMaximum = Maximum != int.MaxValue;

				if (hasMinimum && hasMaximum) return $"{{{Minimum.ToString()},{Maximum.ToString()}}}";
				if (hasMinimum) return $"{{{Minimum.ToString()}...}}";
				return hasMaximum ? $"{{...{Maximum.ToString()}}}" : "";
			}
		}

		private string ArrayRangeString
		{
			get
			{
				if (!Repeating) return "";
				var hasMinimum = MinimumValues != CalculatedMinimumValues;
				var hasMaximum = MaximumValues != uint.MaxValue;

				if (hasMinimum && hasMaximum) return $"<<{MinimumValues.ToString()},{MaximumValues.ToString()}>>";
				if (hasMinimum) return $"<<{MinimumValues.ToString()}...>>";
				return hasMaximum ? $"<<...{MaximumValues.ToString()}>>" : "...";
			}
		}

		public override string ToString()
		{
			return Optional ? $"[{FormattedInternalUsage}]" : $"<{FormattedInternalUsage}>";
		}
	}
}

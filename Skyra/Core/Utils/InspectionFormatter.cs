using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Skyra.Core.Utils
{
	// TODO(kyranet): Tests
	// TODO(kyranet): Circular detection
	// TODO(kyranet): Nested spaces for nested values
	public sealed class InspectionFormatter
	{
		private uint Depth { get; }
		private object? Value { get; }

		public InspectionFormatter(object? value, uint depth)
		{
			Value = value;
			Depth = depth;
		}

		public override string ToString()
		{
			if (Value == null) return "null";

			return Value switch
			{
				string value => Inspect(value),
				byte value => Inspect(value),
				short value => Inspect(value),
				ushort value => Inspect(value),
				int value => Inspect(value),
				uint value => Inspect(value),
				long value => Inspect(value),
				ulong value => Inspect(value),
				float value => Inspect(value),
				double value => Inspect(value),
				decimal value => Inspect(value),
				DateTime value => Inspect(value),
				TimeSpan value => Inspect(value),
				Dictionary<object, object> value => Inspect(value, Depth),
				object[] value => Inspect(value, Depth),
				_ => Inspect(Value, Depth)
			};
		}

		internal static string Inspect(string value)
		{
			return $"\"{value.Replace(@"""", @"\""")}\"";
		}

		internal static string Inspect(short value)
		{
			return value.ToString();
		}

		internal static string Inspect(ushort value)
		{
			return $"{value.ToString()}U";
		}

		internal static string Inspect(int value)
		{
			return value.ToString();
		}

		internal static string Inspect(uint value)
		{
			return $"{value.ToString()}U";
		}

		internal static string Inspect(long value)
		{
			return $"{value.ToString()}L";
		}

		internal static string Inspect(ulong value)
		{
			return $"{value.ToString()}UL";
		}

		internal static string Inspect(float value)
		{
			return $"{value.ToString(CultureInfo.InvariantCulture)}F";
		}

		internal static string Inspect(double value)
		{
			return $"{value.ToString(CultureInfo.InvariantCulture)}D";
		}

		internal static string Inspect(decimal value)
		{
			return $"{value.ToString(CultureInfo.InvariantCulture)}M";
		}

		internal static string Inspect(DateTime value)
		{
			var utc = value.ToUniversalTime();
			return
				$"{utc.Year:0000}-{utc.Month:00}-{utc.Day:00}T{utc.Hour:00}:{utc.Minute:00}:{utc.Second:00}.{utc.Millisecond:0000}Z";
		}

		internal static string Inspect(TimeSpan value)
		{
			return value.ToString();
		}

		internal static string Inspect<TKey, TValue>(Dictionary<TKey, TValue> value, uint depth) where TKey : notnull
		{
			var type = value.GetType();
			var generics = type.GetGenericArguments();
			var keyType = generics[0];
			var valueType = generics[1];
			var isSubclass = IsSubclassOfRawGeneric(typeof(Dictionary<object, object>), type);
			var header = isSubclass ? $"{type.Name} [Dictionary]" : $"Dictionary<{keyType}, {valueType}>";

			if (depth == 0)
			{
				return $"[{header}]";
			}

			var sb = new StringBuilder();
			var count = value.Count;
			var index = 0;
			foreach (var (innerKey, innerValue) in value)
			{
				sb.Append("  ");
				sb.Append(new InspectionFormatter(innerKey, depth - 1));
				sb.Append(" => ");
				sb.Append(new InspectionFormatter(innerValue, depth - 1));
				if (++index < count) sb.Append(",\n");
			}

			return sb.ToString();
		}

		internal static string Inspect<TValue>(TValue[] value, uint depth)
		{
			var type = value.GetType();
			var valueType = type.GetElementType()!;

			if (depth == 0)
			{
				// ReSharper disable once PossibleNullReferenceException
				return $"{valueType.Name}[{value.Length}]";
			}

			var sb = new StringBuilder();
			var count = value.Length;
			var index = 0;
			foreach (var innerValue in value)
			{
				sb.Append(new InspectionFormatter(innerValue, depth - 1));
				if (++index < count) sb.Append(", ");
			}

			return sb.ToString();
		}

		internal static string Inspect(object value, uint depth)
		{
			var type = value.GetType();

			if (depth == 0)
			{
				return $"[{type.Name}]";
			}

			var sb = new StringBuilder();
			sb.Append(type.Name);
			sb.Append(" {\n");

			var properties = type.GetProperties();
			var count = properties.Length;
			var index = 0;
			foreach (var property in properties)
			{
				sb.Append("  ");
				sb.Append(property.Name);
				sb.Append(": ");
				sb.Append(new InspectionFormatter(property.GetValue(value), depth - 1));
				if (++index < count) sb.Append(",\n");
			}

			sb.Append(" }");
			return sb.ToString();
		}

		internal static bool IsSubclassOfRawGeneric(Type generic, Type? toCheck)
		{
			while (toCheck != null && toCheck != typeof(object))
			{
				var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
				if (generic == cur)
				{
					return true;
				}

				toCheck = toCheck.BaseType;
			}

			return false;
		}
	}
}

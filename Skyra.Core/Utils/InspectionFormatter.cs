using System;
using System.Collections;
using System.Globalization;
using System.Text;
using JetBrains.Annotations;

namespace Skyra.Core.Utils
{
	public sealed class InspectionFormatter
	{
		public InspectionFormatter(object? value, uint depth = 1U, [CanBeNull] InspectionFormatter? parent = null)
		{
			Value = value;
			Depth = depth;
			Parent = parent;
			Padding = $"{parent?.Padding}  ";
		}

		private bool Circular
		{
			get
			{
				var parent = Parent;
				while (parent != null)
				{
					if (parent.Value == Value) return true;
					parent = parent.Parent;
				}

				return false;
			}
		}

		private object? Value { get; }
		private InspectionFormatter? Parent { get; }
		private uint Depth { get; }
		private uint NextDepth => Depth == 0U ? 0U : Depth - 1;

		[NotNull]
		private string Padding { get; }

		[Pure]
		[NotNull]
		public override string ToString()
		{
			if (Value == null) return "null";
			if (Circular) return "[Circular]";
			return Value switch
			{
				bool value => Inspect(value),
				char value => Inspect(value),
				string value => Inspect(value),
				sbyte value => Inspect(value),
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
				Type value => Inspect(value),
				Delegate value => Inspect(value),
				Enum value => Inspect(value),
				Array value => Inspect(value),
				IDictionary value => Inspect(value),
				_ => Inspect(Value)
			};
		}

		[Pure]
		[NotNull]
		private string Inspect(bool value)
		{
			return value ? "true" : "false";
		}

		[Pure]
		[NotNull]
		private string Inspect(char value)
		{
			return char.IsSurrogate(value) || char.IsControl(value)
				? $"'\\u{(uint) value:X4}'"
				: $"'{value.ToString()}'";
		}

		[Pure]
		[NotNull]
		private string Inspect([NotNull] string value)
		{
			return $"\"{value.Replace(@"""", @"\""")}\"";
		}

		[Pure]
		[NotNull]
		private string Inspect(sbyte value)
		{
			return $"0x{value:X2}";
		}

		[Pure]
		[NotNull]
		private string Inspect(byte value)
		{
			return $"0x{value:X2}U";
		}

		[Pure]
		[NotNull]
		private string Inspect(short value)
		{
			return value.ToString();
		}

		[Pure]
		[NotNull]
		private string Inspect(ushort value)
		{
			return $"{value.ToString()}U";
		}

		[Pure]
		[NotNull]
		private string Inspect(int value)
		{
			return value.ToString();
		}

		[Pure]
		[NotNull]
		private string Inspect(uint value)
		{
			return $"{value.ToString()}U";
		}

		[Pure]
		[NotNull]
		private string Inspect(long value)
		{
			return $"{value.ToString()}L";
		}

		[Pure]
		[NotNull]
		private string Inspect(ulong value)
		{
			return $"{value.ToString()}UL";
		}

		[Pure]
		[NotNull]
		private string Inspect(float value)
		{
			return $"{value.ToString(CultureInfo.InvariantCulture)}F";
		}

		[Pure]
		[NotNull]
		private string Inspect(double value)
		{
			return $"{value.ToString(CultureInfo.InvariantCulture)}D";
		}

		[Pure]
		[NotNull]
		private string Inspect(decimal value)
		{
			return $"{value.ToString(CultureInfo.InvariantCulture)}M";
		}

		[Pure]
		[NotNull]
		private string Inspect(DateTime value)
		{
			var utc = value.ToUniversalTime();
			return
				$"{utc.Year:0000}-{utc.Month:00}-{utc.Day:00}T{utc.Hour:00}:{utc.Minute:00}:{utc.Second:00}.{utc.Millisecond:000}Z";
		}

		[Pure]
		[NotNull]
		private string Inspect(TimeSpan value)
		{
			return value.ToString();
		}

		[Pure]
		[NotNull]
		private string Inspect([NotNull] Type value)
		{
			return value.FullName! ?? "{INVALID}";
		}

		[Pure]
		[NotNull]
		private string Inspect([NotNull] IDictionary value)
		{
			var type = value.GetType();

			string header;
			if (type.IsConstructedGenericType)
			{
				var generics = type.GetGenericArguments();
				var keyType = generics[0].Name;
				var valueType = generics[1].Name;
				header = $"{CleanName(type.Name)}<{keyType}, {valueType}>";
			}
			else
			{
				header = $"{CleanName(type.Name)} [Dictionary]";
			}

			if (Depth == 0)
			{
				return $"[{header}]";
			}

			if (value.Count == 0)
			{
				return $"{header} {{}}";
			}

			var sb = new StringBuilder();
			sb.Append(header);
			sb.Append(" {" + Environment.NewLine);

			var count = value.Count;
			var index = 0;

#pragma warning disable CS8605
			foreach (DictionaryEntry pair in value)
#pragma warning restore CS8605
			{
				sb.Append(Padding);
				sb.Append(new InspectionFormatter(pair.Key, NextDepth, this));
				sb.Append(" => ");
				sb.Append(new InspectionFormatter(pair.Value, NextDepth, this));
				if (++index < count) sb.Append("," + Environment.NewLine);
			}

			sb.Append(" }");
			return sb.ToString();
		}

		[Pure]
		[NotNull]
		private string Inspect([NotNull] Array value)
		{
			var type = value.GetType();
			var valueType = type.GetElementType()!;

			// ReSharper disable once PossibleNullReferenceException
			var header = $"{CleanName(valueType.Name)}[{value.Length}]";
			if (Depth == 0)
			{
				return header;
			}

			var sb = new StringBuilder();
			sb.Append(header);
			sb.Append(" { ");

			var count = value.Length;
			var index = 0;
			foreach (var innerValue in value)
			{
				sb.Append(new InspectionFormatter(innerValue, NextDepth, this));
				if (++index < count) sb.Append(", ");
			}

			sb.Append(" }");
			return sb.ToString();
		}

		[Pure]
		[NotNull]
		private string Inspect([NotNull] Delegate value)
		{
			var type = value.GetType();
			var sb = new StringBuilder();

			sb.Append(CleanName(type.Name));
			sb.Append("(");

			var parameters = value.Method.GetParameters();
			var count = parameters.Length;
			var index = 0;
			foreach (var parameter in parameters)
			{
				sb.Append(CleanName(parameter.ParameterType.Name));
				sb.Append(" ");
				sb.Append(parameter.Name);
				if (++index < count) sb.Append(", ");
			}

			sb.Append(") => ");
			sb.Append(CleanName(value.Method.ReturnType.Name));

			return sb.ToString();
		}

		[Pure]
		[NotNull]
		private string Inspect([NotNull] Enum value)
		{
			var type = value.GetType();
			return $"{CleanName(type.Name)}.{value.ToString()}";
		}

		[Pure]
		[NotNull]
		private string Inspect([NotNull] object value)
		{
			var type = value.GetType();
			var header = CleanName(type.Name);

			if (Depth == 0)
			{
				return $"[{header}]";
			}

			var properties = type.GetProperties();
			if (properties.Length == 0)
			{
				return $"{header} {{}}";
			}

			var sb = new StringBuilder();
			sb.Append(CleanName(type.Name));
			sb.Append(" {" + Environment.NewLine);
			var count = properties.Length;
			var index = 0;
			foreach (var property in properties)
			{
				sb.Append(Padding);
				sb.Append(property.Name);
				sb.Append(": ");
				sb.Append(new InspectionFormatter(property.GetValue(value), NextDepth, this));
				if (++index < count) sb.Append("," + Environment.NewLine);
			}

			sb.Append(" }");
			return sb.ToString();
		}

		[Pure]
		[NotNull]
		private static string CleanName([NotNull] string name)
		{
			var index = name.IndexOf("`", StringComparison.InvariantCulture);
			return index == -1 ? name : name.Substring(0, index);
		}
	}
}

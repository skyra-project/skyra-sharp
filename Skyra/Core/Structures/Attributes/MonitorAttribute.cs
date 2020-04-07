using System;
using Spectacles.NET.Types;

namespace Skyra.Core.Structures.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class MonitorAttribute : Attribute
	{
		public string? Name { get; set; } = null;
		public MessageType[] AllowedTypes { get; set; } = {MessageType.DEFAULT};
		public bool IgnoreBots { get; set; } = true;
		public bool IgnoreSelf { get; set; } = true;
		public bool IgnoreOthers { get; set; } = true;
		public bool IgnoreWebhooks { get; set; } = true;
		public bool IgnoreEdits { get; set; } = true;
	}
}

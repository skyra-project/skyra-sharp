using Spectacles.NET.Types;

namespace Skyra.Structures
{
	public readonly ref struct MonitorOptions
	{
		public string Name { get; }
		public MessageType[] AllowedTypes { get; }
		public bool IgnoreBots { get; }
		public bool IgnoreSelf { get; }
		public bool IgnoreOthers { get; }
		public bool IgnoreWebhooks { get; }
		public bool IgnoreEdits { get; }

		public MonitorOptions(string name, MessageType[]? allowedTypes = null, bool ignoreBots = true,
			bool ignoreSelf = true, bool ignoreOthers = true, bool ignoreWebhooks = true, bool ignoreEdits = true)
		{
			Name = name;
			AllowedTypes = allowedTypes ?? new[] {MessageType.DEFAULT};
			IgnoreBots = ignoreBots;
			IgnoreSelf = ignoreSelf;
			IgnoreOthers = ignoreOthers;
			IgnoreWebhooks = ignoreWebhooks;
			IgnoreEdits = ignoreEdits;
		}
	}
}

using Skyra.Core.Structures.Base;
using Spectacles.NET.Types;

namespace Skyra.Core.Structures
{
	public struct MonitorInfo
	{
		internal IMonitor Instance { get; set; }
		internal string Name { get; set; }
		internal MessageType[] AllowedTypes { get; set; }
		internal bool IgnoreBots { get; set; }
		internal bool IgnoreSelf { get; set; }
		internal bool IgnoreOthers { get; set; }
		internal bool IgnoreWebhooks { get; set; }
		internal bool IgnoreEdits { get; set; }
	}
}

using System.Reflection;
using Spectacles.NET.Types;

namespace Skyra.Core.Structures
{
	public struct MonitorInfo
	{
		public object Instance { get; set; }
		public MethodInfo Method { get; set; }
		public string Name { get; set; }
		public MessageType[] AllowedTypes { get; set; }
		public bool IgnoreBots { get; set; }
		public bool IgnoreSelf { get; set; }
		public bool IgnoreOthers { get; set; }
		public bool IgnoreWebhooks { get; set; }
		public bool IgnoreEdits { get; set; }
	}
}

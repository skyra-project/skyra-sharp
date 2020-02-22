using Skyra.Core.Structures.Usage;

namespace Skyra.Core.Structures
{
	public struct CommandInfo
	{
		public string Delimiter { get; set; }
		public object Instance { get; set; }
		public string Name { get; set; }
		public CommandUsage Usage { get; set; }
	}
}

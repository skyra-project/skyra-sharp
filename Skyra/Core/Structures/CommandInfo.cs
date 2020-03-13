using Skyra.Core.Structures.Usage;

namespace Skyra.Core.Structures
{
	public struct CommandInfo
	{
		internal string Delimiter { get; set; }
		internal object Instance { get; set; }
		internal string Name { get; set; }
		internal CommandUsage Usage { get; set; }
		internal bool FlagSupport { get; set; }
		internal bool QuotedStringSupport { get; set; }
		internal IInhibitor[] Inhibitors { get; set; }
	}
}

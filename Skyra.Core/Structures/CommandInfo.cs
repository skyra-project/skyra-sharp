using Skyra.Core.Structures.Base;
using Skyra.Core.Structures.Usage;

namespace Skyra.Core.Structures
{
	public struct CommandInfo
	{
		public string Delimiter { get; set; }
		public StructureBase Instance { get; set; }
		public string Name { get; set; }
		public CommandUsage Usage { get; set; }
		public bool FlagSupport { get; set; }
		public bool QuotedStringSupport { get; set; }
		public IInhibitor[] Inhibitors { get; set; }
	}
}

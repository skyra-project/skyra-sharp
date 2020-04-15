using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Base;

namespace Skyra.Worker.Inhibitors
{
	[Inhibitor]
	public class DeveloperInhibitor : StructureBase, IInhibitor
	{
		public DeveloperInhibitor(IClient client) : base(client)
		{
		}

		[NotNull]
		public Task<bool> RunAsync([NotNull] CoreMessage message, CommandInfo command)
		{
			return Task.FromResult(!Client.Owners.Contains(message.AuthorId));
		}
	}
}

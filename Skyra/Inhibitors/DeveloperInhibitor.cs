using System.Linq;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Inhibitors
{
	[Inhibitor]
	public class DeveloperInhibitor : StructureBase, IInhibitor
	{
		public DeveloperInhibitor(Client client) : base(client)
		{
		}

		public Task<bool> RunAsync(CoreMessage message, CommandInfo command)
		{
			return Task.FromResult(!Client.Owners.Contains(message.AuthorId));
		}
	}
}

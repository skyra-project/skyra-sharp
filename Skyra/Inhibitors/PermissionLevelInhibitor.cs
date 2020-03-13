using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Inhibitors
{
	[Inhibitor]
	public class PermissionLevelInhibitor : IInhibitor
	{
		public Task<bool> RunAsync(CoreMessage message, CommandInfo command)
		{
			throw new System.NotImplementedException();
		}
	}
}

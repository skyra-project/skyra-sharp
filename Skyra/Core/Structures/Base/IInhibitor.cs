using System.Threading.Tasks;
using Skyra.Core.Cache.Models;

namespace Skyra.Core.Structures.Base
{
	internal interface IInhibitor
	{
		Task<bool> RunAsync(CoreMessage message, CommandInfo command);
	}
}

using System.Threading.Tasks;
using Skyra.Core.Cache.Models;

namespace Skyra.Core.Structures.Base
{
	public interface IInhibitor
	{
		Task<bool> RunAsync(Message message, CommandInfo command);
	}
}

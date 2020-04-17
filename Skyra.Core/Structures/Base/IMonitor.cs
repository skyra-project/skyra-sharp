using System.Threading.Tasks;
using Skyra.Core.Cache.Models;

namespace Skyra.Core.Structures.Base
{
	public interface IMonitor
	{
		Task RunAsync(Message message);
	}
}

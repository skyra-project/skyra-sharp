using System.Threading.Tasks;
using Skyra.Core.Cache.Models;

namespace Skyra.Core.Structures
{
	public struct InhibitorInfo
	{
		public IInhibitor Instance { get; set; }
		public string Name { get; set; }
	}

	public interface IInhibitor
	{
		Task<bool> RunAsync(CoreMessage message, CommandInfo command);
	}
}

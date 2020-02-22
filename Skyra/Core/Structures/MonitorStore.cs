using System.Linq;
using System.Threading.Tasks;
using Skyra.Core.Structures.Base;
using Spectacles.NET.Types;

namespace Skyra.Core.Structures
{
	public class MonitorStore : Store<Monitor>
	{
		public async Task Run(Message message)
		{
			foreach (var monitor in Values.Where(monitor => monitor.ShouldRun(message)))
				if (!await monitor.Run(message))
					break;
		}
	}
}

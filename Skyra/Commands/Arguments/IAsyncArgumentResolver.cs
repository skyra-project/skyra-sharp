using System.Threading.Tasks;
using Spectacles.NET.Types;

namespace Skyra.Commands.Arguments
{
	public interface IAsyncArgumentResolver<T>
	{
		Task<T> ResolveAsync(Message message);
	}
}

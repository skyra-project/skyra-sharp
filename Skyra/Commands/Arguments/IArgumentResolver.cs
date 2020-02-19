using Spectacles.NET.Types;

namespace Skyra.Commands.Arguments
{
	public interface IArgumentResolver<T>
	{
		T Resolve(Message message);
	}
}

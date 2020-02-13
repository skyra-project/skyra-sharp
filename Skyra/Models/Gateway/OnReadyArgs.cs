using Spectacles.NET.Types;

namespace Skyra.Models.Gateway
{
	public struct OnReadyArgs
	{
		public readonly ReadyDispatch Data;

		public OnReadyArgs(ReadyDispatch data)
		{
			Data = data;
		}
	}
}

using Spectacles.NET.Types;

namespace Skyra.Models.Gateway
{
	public struct OnMessageCreateArgs
	{
		public readonly Message Data;

		public OnMessageCreateArgs(Message data)
		{
			Data = data;
		}
	}
}

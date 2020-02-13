using System;
using Spectacles.NET.Types;

namespace Skyra.Models.Gateway
{
	public class OnMessageCreateArgs : EventArgs
	{
		public readonly Message Data;

		public OnMessageCreateArgs(Message data)
		{
			Data = data;
		}
	}
}

﻿using System;
using Spectacles.NET.Types;

namespace Skyra.Models.Gateway
{
	public class OnReadyArgs : EventArgs
	{
		public readonly ReadyDispatch Data;

		public OnReadyArgs(ReadyDispatch data)
		{
			Data = data;
		}
	}
}

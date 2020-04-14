using System;
using System.Diagnostics.CodeAnalysis;

namespace Skyra.Core.Structures.Exceptions
{
	public class InhibitorException : Exception
	{
		public InhibitorException([NotNull] string description, bool silent = false) : base(description)
		{
			Silent = silent;
		}

		public bool Silent { get; }
	}
}

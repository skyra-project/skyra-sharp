﻿using System.Diagnostics;

namespace Skyra.Docker
{
	internal static class SkyraDocker
	{
		private static void Main()
		{
			var processInfo = new ProcessStartInfo("powershell.exe",
				"-File " + "ps-skyra.ps1")
			{
				CreateNoWindow = false,
				UseShellExecute = false
			};

			var process = Process.Start(processInfo);

			process?.WaitForExit();

			process?.Close();
		}
	}
}

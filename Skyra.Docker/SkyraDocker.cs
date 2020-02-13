using System;
using System.Diagnostics;

namespace Skyra.Docker
{
	internal static class SkyraDocker
	{
		private static void Main(string[] args)
		{
			var processInfo = new ProcessStartInfo("powershell.exe",
				"-File " + Environment.CurrentDirectory + "\\ps-skyra.ps1")
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

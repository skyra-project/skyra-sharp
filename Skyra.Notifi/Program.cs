using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Skyra.NotifI.Core.Models;

namespace Skyra.Notifi
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var webHost = CreateHostBuilder(args).Build();

			using (var scope = webHost.Services.CreateScope())
			{
				var notifiService = scope.ServiceProvider.GetRequiredService<NotifiService>();
				await notifiService.StartAsync();
			}

			await webHost.RunAsync();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
	}
}

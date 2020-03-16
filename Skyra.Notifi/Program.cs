using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skyra.Notifi.Core.Models;

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

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
		}
	}
}

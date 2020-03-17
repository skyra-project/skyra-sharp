using System;
using Microsoft.EntityFrameworkCore;
using Skyra.Notifi.Core.Database.Models.Twitch;

namespace Skyra.Notifi.Core.Database
{
	public sealed class NotifiDatabaseContext : DbContext
	{
		public NotifiDatabaseContext()
		{
		}

		public NotifiDatabaseContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<TwitchSubscription> TwitchSubscriptions { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured) return;

			var user = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
			var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "postgres";
			var host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
			var port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
			var name = Environment.GetEnvironmentVariable("POSTGRES_NAME") ?? "Notifi";

			optionsBuilder.UseNpgsql(
				$"User ID={user};Password={password};Server={host};Port={port};Database={name};Pooling=true;",
				options => options.EnableRetryOnFailure());
		}
	}
}

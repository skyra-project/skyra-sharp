using System;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Skyra.Core.Database.Models;

namespace Skyra.Core.Database
{
	public sealed class SkyraDatabaseContext : DbContext
	{
		public SkyraDatabaseContext()
		{
		}

		public SkyraDatabaseContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Banner> Banners { get; set; } = null!;
		public DbSet<Models.Client> ClientStorage { get; set; } = null!;
		public DbSet<CommandUses> CommandCounter { get; set; } = null!;
		public DbSet<DashboardUser> DashboardUsers { get; set; } = null!;
		public DbSet<Giveaway> Giveaway { get; set; } = null!;
		public DbSet<Guild> Guilds { get; set; } = null!;
		public DbSet<Member> Members { get; set; } = null!;
		public DbSet<Moderation> Moderation { get; set; } = null!;
		public DbSet<Starboard> Starboard { get; set; } = null!;
		public DbSet<User> Users { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured) return;

			var user = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
			var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "";
			var host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
			var port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
			var name = Environment.GetEnvironmentVariable("POSTGRES_NAME") ?? "skyra";

			optionsBuilder.UseNpgsql(
				$"User ID={user};Password={password};Server={host};Port={port};Database={name};Pooling=true;",
				options => options.EnableRetryOnFailure());
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Giveaway>(entity =>
				entity.HasKey(e => new {e.GuildId, e.MessageId}));

			modelBuilder.Entity<Member>(entity =>
			{
				entity.HasKey(e => new {e.GuildId, e.UserId});
				entity.HasIndex(e => e.Points)
					.HasSortOrder(SortOrder.Descending);
			});

			modelBuilder.Entity<Moderation>(entity =>
				entity.HasKey(e => new {e.GuildId, e.CaseId}));

			modelBuilder.Entity<Starboard>(entity =>
				entity.HasKey(e => new {e.GuildId, e.MessageId}));
		}
	}
}

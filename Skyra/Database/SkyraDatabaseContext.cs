using System;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Skyra.Database.Models;

namespace Skyra.Database
{
	public class SkyraDatabaseContext : DbContext
	{
		public SkyraDatabaseContext()
		{
		}

		public SkyraDatabaseContext(DbContextOptions options)
			: base(options)
		{
		}

		public virtual DbSet<Banner> Banners { get; set; }
		public virtual DbSet<Models.Client> ClientStorage { get; set; }
		public virtual DbSet<CommandUsage> CommandCounter { get; set; }
		public virtual DbSet<DashboardUser> DashboardUsers { get; set; }
		public virtual DbSet<Giveaway> Giveaway { get; set; }
		public virtual DbSet<Guild> Guilds { get; set; }
		public virtual DbSet<Member> Members { get; set; }
		public virtual DbSet<Moderation> Moderation { get; set; }
		public virtual DbSet<Starboard> Starboard { get; set; }
		public virtual DbSet<User> Users { get; set; }

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
				entity.HasKey(e => new {e.GuildId, e.MessageId})
					.HasName("giveaway_guild_message_idx"));

			modelBuilder.Entity<Member>(entity =>
			{
				entity.HasKey(e => new {e.GuildId, e.UserId})
					.HasName("members_guild_user_idx");

				entity.HasIndex(e => e.PointCount)
					.HasSortOrder(SortOrder.Descending);
			});

			modelBuilder.Entity<Moderation>(entity =>
				entity.HasKey(e => new {e.GuildId, e.CaseId})
					.HasName("moderation_guild_case_idx"));

			modelBuilder.Entity<Starboard>(entity =>
				entity.HasKey(e => new {e.GuildId, e.MessageId})
					.HasName("starboard_guild_message_idx"));
		}
	}
}

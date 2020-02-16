using System;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Skyra.Database.Models;

namespace Skyra.Database
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

		public DbSet<Banner> Banners { get; set; }
		public DbSet<Models.Client> ClientStorage { get; set; }
		public DbSet<CommandUsage> CommandCounter { get; set; }
		public DbSet<DashboardUser> DashboardUsers { get; set; }
		public DbSet<Giveaway> Giveaway { get; set; }
		public DbSet<Guild> Guilds { get; set; }
		public DbSet<Member> Members { get; set; }
		public DbSet<Moderation> Moderation { get; set; }
		public DbSet<Starboard> Starboard { get; set; }
		public DbSet<User> Users { get; set; }

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

			modelBuilder.Entity<Guild>(entity =>
			{
				entity.OwnsMany(x => x.Actions);
				entity.OwnsMany(x => x.Tags);
				entity.OwnsMany(x => x.CommandAutoDelete);
				entity.OwnsMany(x => x.DisabledCommandChannels);
				entity.OwnsMany(x => x.StickyRoles);
			});

			modelBuilder.Entity<GuildRole>(entity =>
			{
				entity.OwnsMany(x => x.Automatic);
				entity.OwnsMany(x => x.MessageReactions);
				entity.OwnsMany(x => x.UniqueRoleSets);
			});
		}
	}
}

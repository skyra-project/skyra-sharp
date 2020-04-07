using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Skyra.Core.Utils
{
	public static class DbSetExtensions
	{
		[ItemNotNull]
		public static async Task<T> GetOrCreateAsync<T>([NotNull] this DbSet<T> db, ulong id, Func<ulong, T> fn)
			where T : class
		{
			var entity = await db.FindAsync(id);

			if (entity is null)
			{
				entity = fn(id);
				await db.AddAsync(entity);
			}

			return entity;
		}

		public static async Task UpdateOrCreateAsync<T>([NotNull] this DbSet<T> db, ulong id,
			[NotNull] Action<T> modify,
			Func<ulong, T> create) where T : class
		{
			var entity = await db.FindAsync(id) ?? create(id);
			modify(entity);
		}
	}
}

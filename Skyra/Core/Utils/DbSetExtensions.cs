using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Skyra.Core.Utils
{
	public static class DbSetExtensions
	{
		public static async Task<T> GetOrCreateAsync<T>(this DbSet<T> db, ulong id, Func<ulong, T> fn) where T : class
		{
			var entity = await db.FindAsync(id);

			if (entity is null)
			{
				entity = fn(id);
				await db.AddAsync(entity);
			}

			return entity;
		}

		public static async Task UpdateOrCreateAsync<T>(this DbSet<T> db, ulong id, Action<T> modify,
			Func<ulong, T> create) where T : class
		{
			var entity = await db.FindAsync(id) ?? create(id);
			modify(entity);
		}
	}
}

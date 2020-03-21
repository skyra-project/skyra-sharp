using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skyra.Core.Cache.Models;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores.Base
{
	public abstract class CacheStoreBase<T> where T : class, ICoreBaseStructure<T>
	{
		/// <summary>
		///     Create a cache store.
		/// </summary>
		/// <param name="context">The context singleton that manages this instance.</param>
		/// <param name="prefix">The prefix all keys for this store will be prefixed.</param>
		protected CacheStoreBase(CacheClient context, string prefix)
		{
			Context = context;
			Prefix = $"{Context.Prefix}:{prefix}";
		}

		/// <summary>
		///     The context singleton that manages this instance.
		/// </summary>
		protected CacheClient Context { get; }

		/// <summary>
		///     The <see cref="IDatabase" /> instance that handles all queries to Redis.
		/// </summary>
		protected IDatabase Database => Context.Database;

		/// <summary>
		///     The prefix for this cache store.
		/// </summary>
		protected string Prefix { get; }

		/// <summary>
		///     Get an entry from Redis given its identifier and optionally, a parent.
		/// </summary>
		/// <param name="id">The identifier of the object to pull.</param>
		/// <param name="parent">The parent that manages this entry.</param>
		/// <returns>Returns <see cref="T" />, returning null if the value does not exist.</returns>
		public abstract Task<T?> GetAsync(string id, string? parent = null);

		/// <summary>
		///     Get multiple entries from Redis given their identifiers, and optionally, a parent.
		/// </summary>
		/// <param name="ids">An enumerable of string identifiers of the objects to pull.</param>
		/// <param name="parent">The parent which manages this entry.</param>
		/// <returns>Returns an array of <see cref="T" />.</returns>
		public async Task<T[]> GetAsync(IEnumerable<string> ids, string? parent = null)
		{
#pragma warning disable 8603
			return (await Task.WhenAll(ids.Select(id => GetAsync(id, parent)))).Where(x => x != null) as T[];
#pragma warning restore 8603
		}

		/// <summary>
		///     Get all entries from Redis that matches this store's <see cref="Prefix" />, an optionally a parent.
		/// </summary>
		/// <param name="parent">The parent to look up.</param>
		/// <returns>Returns all entries managed by this store in Redis.</returns>
		public abstract Task<T[]> GetAllAsync(string? parent = null);

		/// <summary>
		///     Sets an entry to the Redis database. To set nullable values, please use <see cref="SetNullableAsync" /> instead.
		/// </summary>
		/// <param name="entry">The entry to write to the Redis database.</param>
		/// <param name="parent">The parent that will manage the new entry, if any.</param>
		/// <returns>A <see cref="Task" /> that will resolve when the operation is complete.</returns>
		public abstract Task SetAsync([NotNull] T entry, string? parent = null);

		/// <summary>
		///     Sets multiple entries to the Redis database.
		/// </summary>
		/// <param name="entries">The entries to write to the Redis database.</param>
		/// <param name="parent">The parent that will manage the new entries.</param>
		/// <returns>A <see cref="Task" /> that will resolve when the operation is complete.</returns>
		public abstract Task SetAsync(IEnumerable<T> entries, string? parent = null);

		/// <summary>
		///     Sets an entry to the Redis database, skipping if null, please use <see cref="SetAsync(T,string?)" /> if the value
		///     is never nullable.
		/// </summary>
		/// <param name="entry">The entry to write to the Redis database.</param>
		/// <param name="parent">The parent that will manage this entry, if any.</param>
		/// <returns>A <see cref="Task" /> that will resolve when the operation is complete.</returns>
		public async Task SetNullableAsync(T? entry, string? parent = null)
		{
			if (entry == null) return;
			await SetAsync(entry, parent);
		}

		/// <summary>
		///     Patches an entry by loading the previous entry in memory, then cloning this value and patching it.
		/// </summary>
		/// <param name="entry">The new entry to be patched into the previous value.</param>
		/// <param name="parent">The parent that manages the old and will manage the new entry.</param>
		/// <returns>A tuple, containing the previous (nullable <see cref="T" />) and the new (<see cref="T" />) values.</returns>
		public async Task<(T?, T)> PatchAsync(T entry, string? parent = null)
		{
			var previous = await GetAsync(GetKey(entry), parent);
			if (previous == null)
			{
				await SetAsync(entry, parent);
				return ((T?) null, entry);
			}

			var next = previous.Clone().Patch(entry);
			await SetAsync(next, parent);
			return (previous, next);
		}

		/// <summary>
		///     Deletes an entry from the Redis database.
		/// </summary>
		/// <param name="id">The entry's ID to be deleted.</param>
		/// <param name="parent">The parent that manages the entry to be deleted.</param>
		/// <returns>A <see cref="Task" /> that will resolve when the operation is complete.</returns>
		public abstract Task DeleteAsync(string id, string? parent = null);

		/// <summary>
		///     Deletes multiple entries from the Redis database.
		/// </summary>
		/// <param name="ids">The entries' IDs to be deleted.</param>
		/// <param name="parent">The parent that manages the entries to be deleted.</param>
		/// <returns>A <see cref="Task" /> that will resolve when the operation is complete.</returns>
		public Task DeleteAsync(IEnumerable<string> ids, string? parent = null)
		{
			return Task.WhenAll(ids.Select(id => DeleteAsync(id, parent)));
		}

		/// <summary>
		///     Gets the key from one of the entries this store manages.
		/// </summary>
		/// <param name="value">The value to obtain the ID from.</param>
		/// <returns>The key identifier for this entry.</returns>
		protected abstract string GetKey(T value);

		/// <summary>
		///     Formats a key name given this store's prefix and the parent (if not null).
		/// </summary>
		/// <param name="parent">The parent to prefix this key with.</param>
		/// <returns>The key to be used for all entries in this store.</returns>
		protected string FormatKeyName(string? parent)
		{
			return parent == null ? Prefix : $"{Prefix}:{parent}";
		}

		/// <summary>
		///     Serializes a value into a string for storage.
		/// </summary>
		/// <param name="value">The value to be serialized.</param>
		/// <returns>A <see cref="string" /> representing the JSON-serialized data.</returns>
		protected string SerializeValue(T value)
		{
			return JsonConvert.SerializeObject(value, Formatting.None,
				new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
		}
	}
}

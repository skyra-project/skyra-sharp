using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Skyra.Core.Structures
{
	/// <summary>
	/// A leaky bucket implementation with delay, timespan, and limit.
	/// </summary>
	/// <typeparam name="TKey">The type in which the internal entries will be stored as.</typeparam>
	public sealed class Bucket<TKey> where TKey : notnull
	{
		/// <summary>
		/// The "break" time between invocations.
		/// </summary>
		public TimeSpan Delay { get; set; }

		/// <summary>
		/// How long the bucket will apply for.
		/// </summary>
		public TimeSpan TimeSpan { get; set; }

		/// <summary>
		/// Number of invocations allowed per <seealso cref="TimeSpan"/>.
		/// </summary>
		public uint Limit { get; set; }

		/// <summary>
		/// The internal cache for this instance.
		/// </summary>
		private Dictionary<TKey, BucketEntry> Entries { get; } = new Dictionary<TKey, BucketEntry>();

		/// <summary>
		/// Gets or inserts an entry for a specific user.
		/// </summary>
		/// <param name="key">The key to be used in this bucket.</param>
		/// <returns>A <seealso cref="TimeSpan"/> defining the amount of time to wait before the entry is unlocked.</returns>
		public TimeSpan Take([NotNull] TKey key)
		{
			var time = DateTime.Now;
			var user = Upsert(key);

			// If an extra ticket ends up reaching the limit
			if (user.Tickets + 1 > Limit)
			{
				// and the user is still under cooldown
				if (time < user.SetTime + TimeSpan)
				{
					// then return the remaining time
					return user.SetTime + TimeSpan - time;
				}

				// else reset this bucket's data
				user.Tickets = 0;
				user.SetTime = time;
			}

			// If an extra ticket ends up reaching the delay
			if (time < user.LastTime + Delay)
			{
				// then return the remaining time
				return user.LastTime + Delay - time;
			}

			// else increase tickets by one and set LastTime as now.
			user.Tickets++;
			user.LastTime = time;

			// Entry is not limited, therefore return a zero TimeSpan value.
			return TimeSpan.Zero;
		}

		/// <summary>
		/// Retrieves an entry from the internal dictionary, creating a new one and inserting it if otherwise.
		/// </summary>
		/// <param name="key">The key to be used in this bucket.</param>
		/// <returns>A <seealso cref="BucketEntry"/> entry.</returns>
		private BucketEntry Upsert([NotNull] TKey key)
		{
			if (Entries.TryGetValue(key, out var existingValue)) return existingValue;
			var newValue = new BucketEntry();
			Entries.Add(key, newValue);
			return newValue;
		}

		/// <summary>
		/// The internal entry for the <seealso cref="Bucket{TKey}"/> instance.
		/// </summary>
		private sealed class BucketEntry
		{
			/// <summary>
			/// The last time this entry was mutated.
			/// </summary>
			public DateTime LastTime { get; set; }

			/// <summary>
			/// The last time this entry was reset.
			/// </summary>
			public DateTime SetTime { get; set; }

			/// <summary>
			/// The amount of tickets this entry has. Must always be lower than <seealso cref="Bucket{TKey}.Limit"/>.
			/// </summary>
			public uint Tickets { get; set; }
		}
	}
}

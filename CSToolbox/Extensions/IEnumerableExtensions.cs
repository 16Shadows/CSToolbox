using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSToolbox.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Checks if the <see cref="IEnumerable"/> contains any items.
        /// </summary>
        /// <param name="iterator">The enumerable to check.</param>
        /// <returns>True if <paramref name="iterator"/> contains any items, false otherwise.</returns>
        static public bool Any(this IEnumerable iterator)
        {
            return iterator.GetEnumerator().MoveNext();
        }

		/// <summary>
		/// Checks if two sequences contain the same items regardless of their order.
		/// </summary>
		/// <typeparam name="T">The type of items in the sequence.</typeparam>
		/// <param name="first">The first sequence.</param>
		/// <param name="second">The second sequence.</param>
		/// <returns>True if the sequences contain the same items regardless of their order, false otherwise.</returns>
		/// <remarks>Uses <see cref="EqualityComparer{T}.Default"/> to compare items.</remarks>
        public static bool SequenceEqualsOrderInvariant<T>(this IEnumerable<T> first, IEnumerable<T> second) => SequenceEqualsOrderInvariant(first, second, EqualityComparer<T>.Default);

		/// <summary>
		/// Checks if two sequences contain the same items regardless of their order using specified <see cref="IEqualityComparer{T}"/>.
		/// </summary>
		/// <typeparam name="T">The type of items in the sequence.</typeparam>
		/// <param name="first">The first sequence.</param>
		/// <param name="second">The second sequence.</param>
		/// <param name="equalityComparer">The equality comparer to use.</param>
		/// <returns>True if the sequences contain the same items regardless of their order, false otherwise.</returns>
		public static bool SequenceEqualsOrderInvariant<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> equalityComparer)
		{
			//Code analyzer complains here because TKey shouldn't be nullable (makes sense). However, we need to allow T to be nullable. We are never actually storing nulls in the dictionary.
#pragma warning disable CS8714
			Dictionary<T, int> counters = new Dictionary<T, int>(equalityComparer);
#pragma warning restore CS8714
			int nullCounter = 0;

			int count = 0;

			foreach (var item in first)
			{
				if (item is null)
					nullCounter++;
				else if (counters.TryGetValue(item, out count))
					counters[item] = count + 1;
				else
					counters.Add(item, 1);
			}

			foreach (var item in second)
			{
				if (item is null)
					nullCounter--;
				else if (counters.TryGetValue(item, out count))
				{
					if (count == 1)
						counters.Remove(item);
					else
						counters[item] = count - 1;
				}
				else
					counters.Add(item, -1);
			}

			return nullCounter == 0 && counters.Count == 0;
		}

		public static bool ContainsItemsOrderInvariant<T>(this IEnumerable<T> first, IEnumerable<T> second) => ContainsItemsOrderInvariant(first, second, EqualityComparer<T>.Default);
		public static bool ContainsItemsOrderInvariant<T>(this IEnumerable<T> collection, IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
		{
			//Code analyzer complains here because TKey shouldn't be nullable (makes sense). However, we need to allow T to be nullable. We are never actually storing nulls in the dictionary.
#pragma warning disable CS8714
			Dictionary<T, int> counters = new Dictionary<T, int>(equalityComparer);
#pragma warning restore CS8714
			int nullCounter = 0;

			int count = 0;

			foreach (var item in collection)
			{
				if (item is null)
					nullCounter++;
				else if (counters.TryGetValue(item, out count))
					counters[item] = count + 1;
				else
					counters.Add(item, 1);
			}

			foreach (var item in items)
			{
				if ((item is null && --nullCounter < 0) ||
					(!counters.TryGetValue(item, out count) || (counters[item] = count - 1) < 0))
					return false;
			}

			return true;
		}
    }
}
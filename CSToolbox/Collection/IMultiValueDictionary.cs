using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CSToolbox.Collection
{
	public interface IMultiValueDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, IEnumerable<TValue>>> where TKey : notnull
	{
		IEnumerable<TValue> this[TKey key] { get; }

		IEnumerable<TKey> Keys { get; }

		IEnumerable<TValue> Values { get; }

		void Add(TKey key, TValue value);
		void Add(TKey key, IEnumerable<TValue> range);

		bool ContainsKey(TKey key);
		bool Remove(TKey key);
		bool Remove(TKey key, TValue value);
		bool Remove(TKey key, IEnumerable<TValue> value);
		bool TryGetValue(TKey key, [MaybeNullWhen(false)] out IEnumerable<TValue> value);
	}
}

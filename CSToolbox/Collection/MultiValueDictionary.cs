using CSToolbox.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CSToolbox.Collection
{
	public class MultiValueDictionary<TKey, TValue, BaseDictionary, BaseCollection> : IMultiValueDictionary<TKey, TValue> where TKey : notnull where BaseDictionary : IDictionary<TKey, BaseCollection> where BaseCollection : ICollection<TValue>
	{
		protected readonly BaseDictionary _InternalDictionary;
		protected readonly Func<BaseCollection> _BaseCollectionFactory;

		public MultiValueDictionary(BaseDictionary dict, Func<BaseCollection> baseCollectionFactory)
		{
			_InternalDictionary = dict ?? throw new ArgumentNullException(nameof(dict));
			_BaseCollectionFactory = baseCollectionFactory ?? throw new ArgumentNullException();
		}

		public IEnumerable<TValue> this[TKey key] => _InternalDictionary[key];

		public IEnumerable<TKey> Keys => _InternalDictionary.Keys;

		public IEnumerable<TValue> Values => _InternalDictionary.Values.SelectMany(x => x);

		public int Count => _InternalDictionary.Count;

		public bool IsReadOnly => _InternalDictionary.IsReadOnly;

		public void Add(TKey key, TValue value)
		{
			BaseCollection? list;
			if (!_InternalDictionary.TryGetValue(key, out list))
				_InternalDictionary.Add(key, list = _BaseCollectionFactory());

			list.Add(value);
		}

		public void Add(TKey key, IEnumerable<TValue> range)
		{
			BaseCollection? list;
			if (!_InternalDictionary.TryGetValue(key, out list))
				_InternalDictionary.Add(key, list = _BaseCollectionFactory());

			foreach (var item in range)
				list.Add(item);
		}

		public void Add(KeyValuePair<TKey, IEnumerable<TValue>> item)
		{
			BaseCollection? list;
			if (!_InternalDictionary.TryGetValue(item.Key, out list))
				_InternalDictionary.Add(item.Key, list = _BaseCollectionFactory());

			foreach (var value in item.Value)
				list.Add(value);
		}

		public void Clear()
		{
			_InternalDictionary.Clear();
		}

		public bool Contains(KeyValuePair<TKey, IEnumerable<TValue>> item)
		{
			return _InternalDictionary.TryGetValue(item.Key, out var list) && list.ContainsItemsOrderInvariant(item.Value);
		}

		public bool ContainsKey(TKey key)
		{
			return _InternalDictionary.ContainsKey(key);
		}

		public void CopyTo(KeyValuePair<TKey, IEnumerable<TValue>>[] array, int arrayIndex)
		{
			ArgumentNullException.ThrowIfNull(array);
			if (array.Length - arrayIndex < Count)
				throw new ArgumentException($"Not enough elements in the destination from ${nameof(arrayIndex)}", nameof(array));

			foreach (var item in this)
				array[arrayIndex++] = item;
		}

		public IEnumerator<KeyValuePair<TKey, IEnumerable<TValue>>> GetEnumerator()
			=> _InternalDictionary.Select(x => new KeyValuePair<TKey, IEnumerable<TValue>>(x.Key, x.Value)).GetEnumerator();

		public bool Remove(TKey key)
		{
			return _InternalDictionary.Remove(key);
		}

		public bool Remove(TKey key, TValue value)
		{
			if (!_InternalDictionary.TryGetValue(key, out var list))
				return false;
			
			bool res = list.Remove(value);

			if (list.Count == 0)
				_InternalDictionary.Remove(key);

			return res;
		}

		public bool Remove(TKey key, IEnumerable<TValue> value)
		{
			if (!_InternalDictionary.TryGetValue(key, out var list))
				return false;
			
			bool res = false;
			foreach (var item in value)
				res = list.Remove(item);

			if (list.Count == 0)
				_InternalDictionary.Remove(key);

			return res;
		}

		public bool Remove(KeyValuePair<TKey, IEnumerable<TValue>> item)
			=> Remove(item.Key, item.Value);

		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out IEnumerable<TValue> value)
		{
			bool res = _InternalDictionary.TryGetValue(key, out var list);
			value = list;
			return res;
		}

		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();
	}

	public class MultiValueDictionary<TKey, TValue> : MultiValueDictionary<TKey, TValue, Dictionary<TKey, LinkedList<TValue>>, LinkedList<TValue>> where TKey: notnull
	{
		public MultiValueDictionary(): this(10, EqualityComparer<TKey>.Default) {}
		public MultiValueDictionary(int capacity): this(capacity, EqualityComparer<TKey>.Default) {}
		public MultiValueDictionary(IEqualityComparer<TKey> keyComparer): this(10, keyComparer) {}
		public MultiValueDictionary(int capacity, IEqualityComparer<TKey> keyComparer): base(new Dictionary<TKey, LinkedList<TValue>>(capacity, keyComparer), () => new LinkedList<TValue>()) {}
	}

	public class SortedMultiValueDictionary<TKey, TValue>: MultiValueDictionary<TKey, TValue, SortedDictionary<TKey, LinkedList<TValue>>, LinkedList<TValue>> where TKey: notnull
	{
		public SortedMultiValueDictionary(): this(Comparer<TKey>.Default) {}
		public SortedMultiValueDictionary(IComparer<TKey> keyComparer): base(new SortedDictionary<TKey, LinkedList<TValue>>(keyComparer), () => new LinkedList<TValue>()) {}
	}
}

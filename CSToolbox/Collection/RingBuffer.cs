using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSToolbox.Collection
{
	public class RingBuffer<T> : IEnumerable<T>, IReadOnlyList<T>
	{
		private readonly T[] _Buffer;
		private int _StartIndex = 0;

		public int Count => _Buffer.Length;

		public RingBuffer(int capacity)
		{
			if (capacity < 1)
				throw new ArgumentOutOfRangeException(nameof(capacity), $"{nameof(capacity)} should be greater than 0.");

			_Buffer = new T[capacity];
		}

		public RingBuffer(IEnumerable<T> items)
		{
			_Buffer = items.ToArray();
		}

		public RingBuffer(int capacity, Action<T[]> initializer) : this(capacity)
		{
			ArgumentNullException.ThrowIfNull(initializer);
			initializer(_Buffer);
		}

		public T this[int index]
		{
			get => _Buffer[(_StartIndex + index) % _Buffer.Length];
			set => _Buffer[(_StartIndex + index) % _Buffer.Length] = value;
		}

		public void Add(T item)
		{
			_Buffer[_StartIndex] = item;
			_StartIndex = (_StartIndex + 1) % _Buffer.Length;
		}

		public T[] GetSubarray(int index, int count)
		{
			T[] result = new T[count];

			for (int i = 0; i < count; i++)
				result[i] = _Buffer[ (_StartIndex + index + i) % _Buffer.Length ];

			return result;
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (int i = _StartIndex; i < _Buffer.Length; i++)
				yield return _Buffer[i];

			for (int i = 0; i < _StartIndex; i++)
				yield return _Buffer[i];

			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}

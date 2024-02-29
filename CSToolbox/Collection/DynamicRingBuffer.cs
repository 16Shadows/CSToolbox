using System;
using System.Collections;
using System.Collections.Generic;

namespace CSToolbox.Collection
{
    /// <summary>
    /// A ring buffer which supports dynamic expansion of its size
    /// </summary>
    /// <typeparam name="T">The type to store in ring buffer</typeparam>
    public class DynamicRingBuffer<T> : IEnumerable<T>, IReadOnlyList<T>
    {
        private T[] _Buffer;
        private int _ReadIndex;
        private int _WriteIndex;
        private int _BufferStartIndex;
        private int _Count;
        private int _Capacity;

        public int Capacity => _Capacity;
        public int Count => _Count;
        public bool IsReadOnly => false;
        public bool IsSynchronized => false;
        public object SyncRoot { get; } = new object();

        /// <summary>
        /// Initializes a new <see cref="DynamicRingBuffer{T}"/> with specified initial capacity.
        /// </summary>
        /// <param name="capacity">Initial capacity.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if capacity is less than 1.</exception>
        public DynamicRingBuffer(int capacity)
        {
            if (capacity < 1)
                throw new ArgumentOutOfRangeException(nameof(capacity), $"{nameof(capacity)} should be greater than 0.");

            _Buffer = new T[capacity];
            _ReadIndex = 0;
            _WriteIndex = 0;
            _Capacity = capacity;
            _Count = 0;
            _BufferStartIndex = 0;
        }

        /// <summary>
        /// Initializes a new <see cref="DynamicRingBuffer{T}"/> containing elements from a provided <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">The enumerable to fill this <see cref="DynamicRingBuffer{T}"/> from.</param>
        public DynamicRingBuffer(IEnumerable<T> items) : this(1)
        {
            AddRange(items);
        }

                /// <summary>
        /// Initializes a new <see cref="DynamicRingBuffer{T}"/> containing specified elements.
        /// </summary>
        /// <param name="items">The items to put into this <see cref="DynamicRingBuffer{T}"/>.</param>
        public DynamicRingBuffer(params T[] items) : this((IEnumerable<T>)items) { }

        /// <summary>
        /// Reads or writes stringbuffer at specified index starting from index of the oldest value
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (Count == 0)
                    throw new InvalidOperationException($"{nameof(DynamicRingBuffer<T>)} is empty.");

                return _Buffer[AdjustReadIndex(index)];
            }
            set
            {
                if (Count == 0)
                    throw new InvalidOperationException($"{nameof(DynamicRingBuffer<T>)} is empty.");

                _Buffer[AdjustReadIndex(index)] = value;
            }
        }

        /// <summary>
        /// Writes to the ring buffer. If it is full, overwrites the oldest value.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void Write(T value)
        {
            if (Count < Capacity)
            {
                _Count++;
                _Buffer[AdjustBufferIndex(_WriteIndex++)] = value;
            }
            else
            {
                _Buffer[AdjustWriteIndex(0)] = value;
                _WriteIndex = AdjustWriteIndex(1);
                _ReadIndex = AdjustReadIndex(1);
            }
        }

        /// <summary>
        /// Writes a sequence of elements to the ring buffer. If it is full or becomes full, overwrites values starting from the oldest.
        /// </summary>
        /// <param name="values">The values to write into this <see cref="DynamicRingBuffer{T}"/></param>
        public void WriteRange(IEnumerable<T> values)
        {
            foreach (T value in values)
                Write(value);
        }

        /// <summary>
        /// Writes a sequence of elements to the ring buffer. If it is full or becomes full, overwrites values starting from the oldest.
        /// </summary>
        /// <param name="values">The values to write into this <see cref="DynamicRingBuffer{T}"/></param>
        public void WriteRange(params T[] values) => WriteRange((IEnumerable<T>)values);

        /// <summary>
        /// Pushes a value into the ring buffer. If it is full, expands the ring buffer.
        /// </summary>
        /// <param name="value">The value to push.</param>
        public void Add(T value)
        {
            if (Count >= Capacity)
                ExpandBuffer();

            Write(value);
        }

        /// <summary>
        /// Pushes a sequence of elements into the ring buffer. Expands ring buffer to fit all values without overwriting anything.
        /// </summary>
        /// <param name="values">The values to push</param>
        public void AddRange(IEnumerable<T> values)
        {
            foreach (T value in values)
                Add(value);
        }

        /// <summary>
        /// Pushes a sequence of elements into the ring buffer. Expands ring buffer to fit all values without overwriting anything.
        /// </summary>
        /// <param name="values">The values to push</param>
        public void AddRange(params T[] values) => AddRange((IEnumerable<T>)values);

        /// <summary>
        /// Removes the oldest value from ring buffer and returns it.
        /// </summary>
        /// <returns>The value removed from the buffer.</returns>
        public T Pop()
        {
            T item = this[0];
            _ReadIndex = (_ReadIndex + 1) % Capacity;
            _Count--;
            return item;
        }

        /// <summary>
        /// Removes <paramref name="count"/> oldest values from ring buffer and returns them.
        /// The result is eagerly evaluated.
        /// </summary>
        /// <returns>The value removed from the buffer.</returns>
        public IEnumerable<T> Pop(int count)
        {
            T[] result = new T[count];
            int index = 0;
            while (count-- > 0)
                result[index++] = Pop();

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return this[i];
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Increases the ring buffer's capacity by 1 and, if needed, internal buffers length.
        /// </summary>
        private void ExpandBuffer()
        {
            if (_Capacity < _Buffer.Length)
            {
                _Capacity++;
                return;
            }

            double expansionCoef = _Buffer.Length < 256 ? 2 :
                                    _Buffer.Length < 1024 ? 1.2 : 1.05;

            T[] newBuffer = new T[(int)Math.Ceiling(_Buffer.Length * expansionCoef)];
            int index = 0;
            foreach (T value in this)
                newBuffer[index++] = value;

            _Capacity++;
            _Buffer = newBuffer;
            _BufferStartIndex = 0;
            _ReadIndex = 0;
            _WriteIndex = index;
        }

        /// <summary>
        /// Computes actual index in <see cref="_Buffer"/> based on ring buffer's read index.
        /// </summary>
        /// <param name="index">Read index to compute actual index for.</param>
        /// <returns>The actual index of the element in the <see cref="_Buffer"/>.</returns>
        private int AdjustReadIndex(int index) => AdjustBufferIndex(_ReadIndex + index % _Count);
        /// <summary>
        /// Computes actual index in <see cref="_Buffer"/> based on ring buffer's write index.
        /// </summary>
        /// <param name="index">Write index to compute actual index for.</param>
        /// <returns>The actual index of the element in the <see cref="_Buffer"/>.</returns>
        private int AdjustWriteIndex(int index) => AdjustBufferIndex(_WriteIndex + index % _Count);
        /// <summary>
        /// Computes actual index in <see cref="_Buffer"/> based on ring buffer's buffer index.
        /// </summary>
        /// <param name="index">Buffer index to compute actual index for.</param>
        /// <returns>The actual index of the element in the <see cref="_Buffer"/>.</returns>
        private int AdjustBufferIndex(int index) => (_BufferStartIndex + index % _Capacity) % _Buffer.Length;

        /// <summary>
        /// Removes all items from this ring buffer without changing its capacity
        /// </summary>
        public void Clear()
        {
            _BufferStartIndex = 0;
            _WriteIndex = 0;
            _ReadIndex = 0;
            _Count = 0;
        }

        /// <summary>
        /// Shrinks this ring buffer's capacity to match target capacity.
        /// If <paramref name="targetCapacity"/> is less than <see cref="Count"/>, oldest items will be removed to free up space.
        /// </summary>
        /// <param name="targetCapacity"></param>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown if <paramref name="targetCapacity"/> is less than 1.</exception>
        public void Shrink(int targetCapacity)
        {
            if (targetCapacity < 1)
                throw new ArgumentOutOfRangeException(nameof(targetCapacity));

            while (Count > targetCapacity)
                _ = Pop();

            _Capacity = targetCapacity;
        }
    }
}

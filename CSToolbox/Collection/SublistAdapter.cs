using System;
using System.Collections.Generic;

namespace CSToolbox.Collection
{
	/// <summary>
	/// Implements a 'view' of an adapted list. 
	/// This list acts as an adapter for a list of <typeparamref name="TFrom"/> presenting only items of type <typeparamref name="TTo"/>.
	/// All changes made to this list are reflected in the adapted list and vice versa.
	/// </summary>
	/// <typeparam name="TFrom">The type of items in the adapted list.</typeparam>
	/// <typeparam name="TTo">The type of items to present in this view.</typeparam>
	public class SublistAdapter<TFrom, TTo> : SubcollectionAdapter<TFrom, TTo>, IList<TTo>, IReadOnlyList<TTo> where TTo : TFrom
	{
		protected readonly IList<TFrom> _AdaptedList;

		/// <summary>
		/// Creates an instance of <see cref="SublistAdapter{TFrom, TTo}"/> which adapts <paramref name="adaptedCollection"/>.
		/// </summary>
		/// <param name="adaptedCollection">The list to adapt.</param>
		public SublistAdapter(IList<TFrom> adaptedCollection) : base(adaptedCollection)
		{
			ArgumentNullException.ThrowIfNull(adaptedCollection);

			_AdaptedList = adaptedCollection;
		}

		/// <summary>
		/// Convert an index in this view into an index in the adapted list.
		/// </summary>
		/// <param name="index">The index to adapt.</param>
		/// <returns>The index in the adapted collection of the item located at <paramref name="index"/> in this view.</returns>
		/// <exception cref="ArgumentOutOfRangeException">Is thrown if the <paramref name="index"/> is less than 0 or greated than or equals to <see cref="SubcollectionAdapter{TFrom, TTo}.Count"/></exception>
		protected int GetAdaptedIndex(int index)
		{
			if (index < 0)
					throw new ArgumentOutOfRangeException(nameof(index));

			int i;
			for (i = 0; i < _AdaptedList.Count; i++)
			{
				if (_AdaptedList[i] is not TTo)
					continue;
					
				index--;
				if (index < 0)
					break;
			}

			if (index >= 0)
				throw new ArgumentOutOfRangeException(nameof(index));

			return i;
		}

		/// <summary>
		/// An indexer into this view. Each access causes a partial iteration of the adapted list to reach the desired index.
		/// </summary>
		/// <param name="index">The index of an item in this view.</param>
		/// <returns></returns>
		public TTo this[int index]
		{
			get => (TTo)_AdaptedList[GetAdaptedIndex(index)]!;
			set => _AdaptedList[GetAdaptedIndex(index)] = value;
		}

		/// <summary>
		/// Finds an index of an item in this view.
		/// </summary>
		/// <param name="item">The item to find the index for.</param>
		/// <returns>The index of the item, or -1 if the item wasn't found.</returns>
		public int IndexOf(TTo item)
		{
			int internalIndex = 0;
			for (int i = 0; i < _AdaptedList.Count; i++)
			{
				if (_AdaptedList[i] is not TTo)
					continue;
				else if (EqualityComparer<TTo>.Default.Equals(item, (TTo?)_AdaptedList[i]))
					return internalIndex;

				internalIndex++;
			}
			return -1;
		}

		/// <summary>
		/// Inserts an item into the adapted collection so that it would be at <paramref name="index"/> in this view.
		/// No guarantees are made regarding the actual index in the adapted collection.
		/// This method causes a partial iteration of the adapted collection.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="item"></param>
		public void Insert(int index, TTo item)
		{
			_AdaptedList.Insert(GetAdaptedIndex(index), item);
		}

		/// <summary>
		/// Removes an item from in adapted collection located at <paramref name="index"/> in this view.
		/// </summary>
		/// <param name="index">The index of an item in this view.</param>
		public void RemoveAt(int index)
		{
			_AdaptedList.RemoveAt(GetAdaptedIndex(index));
		}

		/// <summary>
		/// Removes all items of type <typeparamref name="TTo"/> from the adapted collection.
		/// This implementation causes an enumeration of the adapted list.
		/// </summary>
		public override void Clear()
		{
			for (int i = 0; i < _AdaptedList.Count;)
			{
				if (_AdaptedList[i] is TTo)
					_AdaptedList.RemoveAt(i);
				else
					i++;
			}
		}
	}
}

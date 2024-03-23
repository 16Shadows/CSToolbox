using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSToolbox.Collection
{
	/// <summary>
	/// Implements a 'view' of an adapted collection. 
	/// This collection acts as an adapter for a collection of <typeparamref name="TFrom"/> presenting only items of type <typeparamref name="TTo"/>.
	/// All changes made to this collection are reflected in the adapted collection and vice versa.
	/// </summary>
	/// <typeparam name="TFrom">The type of items in the adapted collection.</typeparam>
	/// <typeparam name="TTo">The type of items to present in this view.</typeparam>
	public class SubcollectionAdapter<TFrom, TTo> : ICollection<TTo>, IReadOnlyCollection<TTo> where TTo : TFrom
	{
		protected readonly ICollection<TFrom> _AdaptedCollection;

		/// <summary>
		/// Creates an instance of <see cref="SubcollectionAdapter{TFrom, TTo}"/> which adapts <paramref name="adaptedCollection"/>.
		/// </summary>
		/// <param name="adaptedCollection">The collection to adapt.</param>
		public SubcollectionAdapter(ICollection<TFrom> adaptedCollection)
		{
			ArgumentNullException.ThrowIfNull(adaptedCollection);

			_AdaptedCollection = adaptedCollection;
		}

		/// <summary>
		/// Computes number of items of type <typeparamref name="TTo"/> in the adapted collection.
		/// Iterates the adapted collection.
		/// </summary>
		public int Count => _AdaptedCollection.Count(x => x is TTo);

		/// <summary>
		/// Returns the same value as <see cref="IsReadOnly"/> in adapted collection.
		/// </summary>
		public bool IsReadOnly => _AdaptedCollection.IsReadOnly;

		/// <summary>
		/// Adds an item into the adapted collection.
		/// </summary>
		/// <param name="item">The item to add.</param>
		public virtual void Add(TTo item)
		{
			_AdaptedCollection.Add(item);
		}

		/// <summary>
		/// Removes all items of type <typeparamref name="TTo"/> from the adapted collection.
		/// Standard implementation causes an enumeration of the adapted collection and allocates an array of size <see cref="Count"/>.
		/// </summary>
		public virtual void Clear()
		{
			TTo[] items = this.ToArray();
			foreach (TTo item in items)
				_AdaptedCollection.Remove(item);
		}

		/// <summary>
		/// Checks whether the adapted collection contains a given item of type <typeparamref name="TTo"/>.
		/// </summary>
		/// <param name="item">The item to check for.</param>
		/// <returns>True if the collection contains the item, false otherwise.</returns>
		public virtual bool Contains(TTo item)
		{
			return _AdaptedCollection.Contains(item);
		}

		/// <summary>
		/// Copies only items of type <typeparamref name="TTo"/> from the adapted collection.
		/// </summary>
		/// <param name="array">The array to write items to.</param>
		/// <param name="arrayIndex">The index to start from.</param>
		public virtual void CopyTo(TTo[] array, int arrayIndex)
		{
			foreach(TTo item in this)
				array[arrayIndex++] = item;
		}

		/// <summary>
		/// Returns an enumerator of items of type <typeparamref name="TTo"/> in the adapted collection.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public virtual IEnumerator<TTo> GetEnumerator()
		{
			return _AdaptedCollection.Where(x => x is TTo).Cast<TTo>().GetEnumerator();
		}

		/// <summary>
		/// Removes an item of type <typeparamref name="TTo"/> from the adapted collection.
		/// </summary>
		/// <param name="item">The item to remove.</param>
		/// <returns>True if the item was removed, false otherwise.</returns>
		public virtual bool Remove(TTo item)
		{
			return _AdaptedCollection.Remove(item);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}

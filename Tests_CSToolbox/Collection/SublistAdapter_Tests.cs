using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSToolbox.Collection;
using CSToolbox.Extensions;

namespace CSToolbox.Collection.Tests
{
	[TestClass()]
	public class SublistAdapter_Tests
	{
		[TestMethod()]
		public void Add_Test()
		{
			List<object> adaptedCollection = new List<object>();
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			adapter.Add(1);

			CollectionAssert.Contains(adaptedCollection, 1);
		}

		[TestMethod()]
		public void Count_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			Assert.AreEqual(2, adapter.Count);
		}

		[TestMethod()]
		public void IsReadOnly_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			Assert.AreEqual(((ICollection<object>)adaptedCollection).IsReadOnly, adapter.IsReadOnly);
		}

		[TestMethod()]
		public void Clear_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			adapter.Clear();

			Assert.AreEqual(0, adapter.Count);
			Assert.AreEqual(3, adaptedCollection.Count);
			Assert.IsFalse(adaptedCollection.Any(x => x is int));
		}

		[TestMethod()]
		public void Contains_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			Assert.IsTrue(adapter.Contains(5));
		}

		[TestMethod()]
		public void Enumerator_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			Assert.IsTrue((new int[] {5, 17}).SequenceEqual(adapter));
		}

		[TestMethod()]
		public void CopyTo_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			int[] output = new int[adaptedCollection.Count];
			adapter.CopyTo(output, 0);

			Assert.IsTrue((new int[] {5, 17}).SequenceEqual(adapter));
		}

		[TestMethod()]
		public void Remove_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			Assert.IsTrue(adapter.Remove(5));
			Assert.IsFalse(adapter.Remove(5));

			Assert.AreEqual(1, adapter.Count);
			Assert.AreEqual(4, adaptedCollection.Count);
			Assert.IsFalse(adaptedCollection.Contains(5));
		}

		[TestMethod()]
		public void Indexer_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => adapter[-1]);
			Assert.AreEqual(5, adapter[0]);
			Assert.AreEqual(17, adapter[1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => adapter[2]);

			adapter[0] = 22;

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => adapter[-1]);
			Assert.AreEqual(22, adapter[0]);
			Assert.AreEqual(17, adapter[1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => adapter[2]);
		}

		[TestMethod()]
		public void IndexOf_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			Assert.AreEqual(1, adapter.IndexOf(17));
		}

		[TestMethod()]
		public void Insert_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 5, 2.5f, 22.3, 17 };
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			adapter.Insert(1, 14);

			Assert.AreEqual(6, adaptedCollection.Count);
			Assert.AreEqual(3, adapter.Count);
			Assert.IsTrue(adaptedCollection.Contains(14));
			Assert.IsTrue(adapter.IndexOf(5) < adapter.IndexOf(14));
			Assert.IsTrue(adapter.IndexOf(14) < adapter.IndexOf(17));
			Assert.IsTrue(adaptedCollection.IndexOf(5) < adaptedCollection.IndexOf(14));
			Assert.IsTrue(adaptedCollection.IndexOf(14) < adaptedCollection.IndexOf(17));
		}

		[TestMethod()]
		public void RemoveAt_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SublistAdapter<object, int> adapter = new SublistAdapter<object, int>(adaptedCollection);

			adapter.RemoveAt(0);

			Assert.AreEqual(4, adaptedCollection.Count);
			Assert.AreEqual(1, adapter.Count);
			Assert.AreEqual(adapter.First(), 17);
		}
	}
}
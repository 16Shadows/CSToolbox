using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSToolbox.Collection;
using CSToolbox.Extensions;

namespace CSToolbox.Collection.Tests
{
	[TestClass()]
	public class SubcollectionAdapter_Tests
	{
		[TestMethod()]
		public void Add_Test()
		{
			List<object> adaptedCollection = new List<object>();
			SubcollectionAdapter<object, int> adapter = new SubcollectionAdapter<object, int>(adaptedCollection);

			adapter.Add(1);

			CollectionAssert.Contains(adaptedCollection, 1);
		}

		[TestMethod()]
		public void Count_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SubcollectionAdapter<object, int> adapter = new SubcollectionAdapter<object, int>(adaptedCollection);

			Assert.AreEqual(2, adapter.Count);
		}

		[TestMethod()]
		public void IsReadOnly_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SubcollectionAdapter<object, int> adapter = new SubcollectionAdapter<object, int>(adaptedCollection);

			Assert.AreEqual(((ICollection<object>)adaptedCollection).IsReadOnly, adapter.IsReadOnly);
		}

		[TestMethod()]
		public void Clear_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SubcollectionAdapter<object, int> adapter = new SubcollectionAdapter<object, int>(adaptedCollection);

			adapter.Clear();

			Assert.AreEqual(0, adapter.Count);
			Assert.AreEqual(3, adaptedCollection.Count);
			Assert.IsFalse(adaptedCollection.Any(x => x is int));
		}

		[TestMethod()]
		public void Contains_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SubcollectionAdapter<object, int> adapter = new SubcollectionAdapter<object, int>(adaptedCollection);

			Assert.IsTrue(adapter.Contains(5));
		}

		[TestMethod()]
		public void Enumerator_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SubcollectionAdapter<object, int> adapter = new SubcollectionAdapter<object, int>(adaptedCollection);

			Assert.IsTrue((new int[] {5, 17}).SequenceEqualsOrderInvariant(adapter));
		}

		[TestMethod()]
		public void CopyTo_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SubcollectionAdapter<object, int> adapter = new SubcollectionAdapter<object, int>(adaptedCollection);

			int[] output = new int[adaptedCollection.Count];
			adapter.CopyTo(output, 0);

			Assert.IsTrue((new int[] {5, 17}).SequenceEqualsOrderInvariant(adapter));
		}

		[TestMethod()]
		public void Remove_Test()
		{
			List<object> adaptedCollection = new List<object>() { "hello", 2.5f, 22.3, 5, 17 };
			SubcollectionAdapter<object, int> adapter = new SubcollectionAdapter<object, int>(adaptedCollection);

			Assert.IsTrue(adapter.Remove(5));
			Assert.IsFalse(adapter.Remove(5));

			Assert.AreEqual(1, adapter.Count);
			Assert.AreEqual(4, adaptedCollection.Count);
			Assert.IsFalse(adaptedCollection.Contains(5));
		}
	}
}
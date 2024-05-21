using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace CSToolbox.Extensions.Tests
{
	[TestClass()]
	public class IEnumerableExtensions_Tests
	{
		[TestMethod()]
		public void SequenceEqualsOrderInvariant_ValidTest()
		{
			List<int> a = new List<int>() { 2, 3, 4, 5, 5, 4 };
			List<int> b = new List<int>() { 5, 4, 3, 2, 4, 5 };

			Assert.IsTrue(a.SequenceEqualsOrderInvariant(b));
			Assert.IsTrue(b.SequenceEqualsOrderInvariant(a));
		}

		[TestMethod()]
		public void SequenceEqualsOrderInvariant_InvalidTest()
		{
			List<int> a = new List<int>() { 2, 3, 4, 4, 4, 4 };
			List<int> b = new List<int>() { 5, 4, 3, 2, 4, 5 };

			Assert.IsFalse(a.SequenceEqualsOrderInvariant(b));
			Assert.IsFalse(b.SequenceEqualsOrderInvariant(a));
		}

		[TestMethod()]
		public void SequenceEqualsOrderInvariant_InvalidTest2()
		{
			List<int> a = new List<int>() { 2, 3, 4, 4, 4, 4, 5, 4 };
			List<int> b = new List<int>() { 5, 4, 3, 2, 4, 5 };

			Assert.IsFalse(a.SequenceEqualsOrderInvariant(b));
			Assert.IsFalse(b.SequenceEqualsOrderInvariant(a));
		}

		private class TestEqualityComparer : IEqualityComparer<int>
		{
			public bool Equals(int x, int y)
			{
				return true;
			}

			public int GetHashCode([DisallowNull] int obj)
			{
				return 0;
			}
		}

		[TestMethod()]
		public void SequenceEqualsOrderInvariant_EqualityComparer_ValidTest()
		{
			List<int> a = new List<int>() { 2, 3, 4, 5, 5, 4 };
			List<int> b = new List<int>() { 5, 4, 3, 2, 4, 5 };

			Assert.IsTrue(a.SequenceEqualsOrderInvariant(b, new TestEqualityComparer()));
			Assert.IsTrue(b.SequenceEqualsOrderInvariant(a, new TestEqualityComparer()));
		}

		[TestMethod()]
		public void SequenceEqualsOrderInvariant_EqualityComparer_ValidTest2()
		{
			List<int> a = new List<int>() { 2, 3, 4, 5, 5, 4 };
			List<int> b = new List<int>() { 5, 4, 3, 2, 7, 6 };

			Assert.IsTrue(a.SequenceEqualsOrderInvariant(b, new TestEqualityComparer()));
			Assert.IsTrue(b.SequenceEqualsOrderInvariant(a, new TestEqualityComparer()));
		}

		[TestMethod()]
		public void SequenceEqualsOrderInvariant_EqualityComparer_InvalidTest()
		{
			List<int> a = new List<int>() { 2, 3, 4, 5, 5, 4 };
			List<int> b = new List<int>() { 5, 4, 3, 2, 4, 5, 7, 6 };

			Assert.IsFalse(a.SequenceEqualsOrderInvariant(b, new TestEqualityComparer()));
			Assert.IsFalse(b.SequenceEqualsOrderInvariant(a, new TestEqualityComparer()));
		}

		[TestMethod()]
		public void ContainsItemsOrderInvariant_ValidTest()
		{
			List<int> a = new List<int>() { 2, 3, 4, 5, 5, 4 };
			List<int> b = new List<int>() { 5, 5, 3, 2 };

			Assert.IsTrue(a.ContainsItemsOrderInvariant(b));
		}

		[TestMethod()]
		public void ContainsItemsOrderInvariant_InvalidTest()
		{
			List<int> a = new List<int>() { 2, 3, 4, 5, 2, 4 };
			List<int> b = new List<int>() { 5, 5, 3, 2 };

			Assert.IsFalse(a.ContainsItemsOrderInvariant(b));
		}


		[TestMethod()]
		public void ContainsItemsOrderInvariant_EqualityComparer_ValidTest()
		{
			List<int> a = new List<int>() { 2, 3, 4, 5, 5, 4 };
			List<int> b = new List<int>() { 5, 5, 3, 2 };

			Assert.IsTrue(a.ContainsItemsOrderInvariant(b, new TestEqualityComparer()));
		}

		[TestMethod()]
		public void ContainsItemsOrderInvariant_EqualityComparer_InvalidTest()
		{
			List<int> a = new List<int>() { 2, 3, 4, 5, 2, 4 };
			List<int> b = new List<int>() { 5, 5, 3, 2, 1, 1, 1, 1 };

			Assert.IsFalse(a.ContainsItemsOrderInvariant(b, new TestEqualityComparer()));
		}
	}
}

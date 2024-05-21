using CSToolbox.Collection;
using CSToolbox.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_CSToolbox.Collection
{
	[TestClass()]
	public class MultiValueDictionary_Tests
	{
		[TestMethod()]
		public void MultiValueDictionary_Add_Item_Test()
		{
			int[] arr = new int[] { 1, 2 };
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, 1);
			dict.Add(1, 2);

			Assert.IsTrue(dict.ContainsKey(1));
			Assert.IsTrue(dict[1].SequenceEqualsOrderInvariant(arr));
		}

		[TestMethod()]
		public void MultiValueDictionary_Add_Collection_Test()
		{
			int[] arr = new int[] { 1, 2 };
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, arr);

			Assert.IsTrue(dict.ContainsKey(1));
			Assert.IsTrue(dict[1].SequenceEqualsOrderInvariant(arr));
		}

		[TestMethod()]
		public void MultiValueDictionary_Add_KVP_Test()
		{
			int[] arr = new int[] { 1, 2 };
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(new KeyValuePair<int, IEnumerable<int>>(1, arr));

			Assert.IsTrue(dict.ContainsKey(1));
			Assert.IsTrue(dict[1].SequenceEqualsOrderInvariant(arr));
		}

		[TestMethod()]
		public void MultiValueDictionary_Count_Test()
		{
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, 1);
			dict.Add(1, 2);
			dict.Add(2, 2);

			Assert.AreEqual(2, dict.Count);
		}

		[TestMethod()]
		public void MultiValueDictionary_Keys_Test()
		{
			int[] arr = new int[] { 1, 2 };
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, 1);
			dict.Add(1, 2);
			dict.Add(2, 3);

			Assert.IsTrue(dict.Keys.SequenceEqualsOrderInvariant(arr));
		}

		[TestMethod()]
		public void MultiValueDictionary_Values_Test()
		{
			int[] arr = new int[] { 1, 2, 3 };
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, 1);
			dict.Add(1, 2);
			dict.Add(2, 3);

			Assert.IsTrue(dict.Values.SequenceEqualsOrderInvariant(arr));
		}

		[TestMethod()]
		public void MultiValueDictionary_Clear_Test()
		{
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, 1);
			dict.Add(1, 2);
			dict.Add(2, 3);
			dict.Clear();

			Assert.AreEqual(0, dict.Count);
		}

		[TestMethod()]
		public void MultiValueDictionary_Contains_Test()
		{
			int[] arr = new int[] { 1, 2 };
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, 1);
			dict.Add(1, 3);
			dict.Add(1, 2);
			dict.Add(2, 3);

			Assert.IsTrue(dict.Contains(new KeyValuePair<int, IEnumerable<int>>(1, arr)));
		}

		class KVPComparer : IEqualityComparer<KeyValuePair<int, IEnumerable<int>>>
		{
			public bool Equals(KeyValuePair<int, IEnumerable<int>> x, KeyValuePair<int, IEnumerable<int>> y)
			{
				return x.Key == y.Key && x.Value.SequenceEqualsOrderInvariant(y.Value);
			}

			public int GetHashCode([DisallowNull] KeyValuePair<int, IEnumerable<int>> obj)
			{
				return obj.GetHashCode();
			}
		}

		[TestMethod()]
		public void MultiValueDictionary_Enumerator_Test()
		{
			KeyValuePair<int, IEnumerable<int>>[] vals = new KeyValuePair<int, IEnumerable<int>>[]
			{
				new KeyValuePair<int, IEnumerable<int>>(1, new int[] {1, 2, 3}),
				new KeyValuePair<int, IEnumerable<int>>(2, new int[] {3})
			};
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			foreach (var item in vals)
				dict.Add(item.Key, item.Value);

			foreach (var item in dict)
				Assert.IsTrue(vals.Contains(item, new KVPComparer()));
		}

		[TestMethod()]
		public void MultiValueDictionary_Remove_Key_Test()
		{
			int[] arr = new int[] { 1, 2, 3 };
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, 1);
			dict.Add(1, 3);
			dict.Add(1, 2);
			dict.Add(2, 3);

			Assert.IsTrue(dict.Remove(2));
			Assert.IsFalse(dict.Remove(2));
			Assert.IsTrue(dict.Values.SequenceEqualsOrderInvariant(arr));
		}

		[TestMethod()]
		public void MultiValueDictionary_Remove_KeyValue_Test()
		{
			int[] arr = new int[] { 1, 3, 3 };
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, 1);
			dict.Add(1, 3);
			dict.Add(1, 2);
			dict.Add(2, 3);

			Assert.IsTrue(dict.Remove(1, 2));
			Assert.IsFalse(dict.Remove(1, 2));
			Assert.IsTrue(dict.Values.SequenceEqualsOrderInvariant(arr));
		}

		[TestMethod()]
		public void MultiValueDictionary_Remove_KeyValueRange_Test()
		{
			int[] arr = new int[] { 3, 3 };
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, 1);
			dict.Add(1, 3);
			dict.Add(1, 2);
			dict.Add(2, 3);

			Assert.IsTrue(dict.Remove(1, new int[] { 1, 2 }));
			Assert.IsFalse(dict.Remove(1, 2));
			Assert.IsTrue(dict.Values.SequenceEqualsOrderInvariant(arr));
		}

		[TestMethod()]
		public void MultiValueDictionary_Remove_KVP_Test()
		{
			int[] arr = new int[] { 3, 3 };
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, 1);
			dict.Add(1, 3);
			dict.Add(1, 2);
			dict.Add(2, 3);

			Assert.IsTrue(dict.Remove(new KeyValuePair<int, IEnumerable<int>>(1, new int[] { 1, 2 })));
			Assert.IsFalse(dict.Remove(new KeyValuePair<int, IEnumerable<int>>(1, new int[] { 2 })));
			Assert.IsTrue(dict.Values.SequenceEqualsOrderInvariant(arr));
		}

		[TestMethod()]
		public void MultiValueDictionary_TryGetValue_Test()
		{
			int[] arr = new int[] { 3 };
			MultiValueDictionary<int, int> dict = new MultiValueDictionary<int, int>();

			dict.Add(1, 1);
			dict.Add(1, 3);
			dict.Add(1, 2);
			dict.Add(2, 3);

			Assert.IsTrue(dict.TryGetValue(2, out var list));
			Assert.IsTrue(list.SequenceEqualsOrderInvariant(arr));
		}
	}
}

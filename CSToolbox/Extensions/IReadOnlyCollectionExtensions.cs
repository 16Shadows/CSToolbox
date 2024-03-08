using System.Collections.Generic;
using System.Linq;

namespace CSToolbox.Extensions
{
	public static class IReadOnlyCollectionExtensions
	{
		public static T RandomElement<T>(this IReadOnlyCollection<T> collection)
			=>collection.Skip(System.Random.Shared.Next(0, collection.Count)).First();
	}
}

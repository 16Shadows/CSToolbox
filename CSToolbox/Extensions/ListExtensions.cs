using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSToolbox.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Removes first occurence of a matching item in the list
        /// </summary>
        /// <typeparam name="T">The type of items store in the list</typeparam>
        /// <param name="list">The list</param>
        /// <param name="predicate">A predicate to determine a matching item</param>
        public static void Remove<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (!predicate(list[i]))
                    continue;
                list.RemoveAt(i);
                return;
            }
        }

        public static int LastIndexOf<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (int i = list.Count-1; i >= 0; i--)
            {
                if (!predicate(list[i]))
                    continue;
                return i;
            }
            return -1;
        }

        /// <summary>
        /// Generates a string from a list adding separator in-between. Uses <see cref="object.ToString"/> to convert items within the list into strings.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The list to convert.</param>
        /// <param name="separator">The character to use as separator between string representation of objects in the list.</param>
        /// <returns></returns>
        public static string ToString<T>(this IList<T> list, char separator) => ToString(list, separator.ToString());

        /// <summary>
        /// Generates a string from a list adding separator in-between. Uses <see cref="object.ToString"/> to convert items within the list into strings.
        /// </summary>
        /// <typeparam name="T">The type of items in the list.</typeparam>
        /// <param name="list">The list to convert.</param>
        /// <param name="separator">The string to use as separator between string representation of objects in the list.</param>
        /// <returns></returns>
		public static string ToString<T>(this IList<T> list, string separator)
		{
            ArgumentNullException.ThrowIfNull(list);
            ArgumentNullException.ThrowIfNull(separator);

			if (list.Count == 0)
				return string.Empty;

			StringBuilder sb = new StringBuilder(list.Count * 10);

			sb.Append(list[0]);
			for (int i = 1;  i < list.Count; i++)
			{
				sb.Append(separator);
				sb.Append(list[i]);
			}

			return sb.ToString();
		}
    }
}

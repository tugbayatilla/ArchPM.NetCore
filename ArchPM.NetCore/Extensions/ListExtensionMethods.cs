using ArchPM.NetCore.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ListExtensionMethods
    {
        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="projection"></param>
        public static void ModifyEach<T>(this IList<T> source, Func<T, T> projection)
        {
            for (int i = 0; i < source.Count; i++)
            {
                source[i] = projection(source[i]);
            }
        }

        /// <summary>
        /// Tests if provided list of objects is not null or empty.
        /// Throws ValidationException if it is null or is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects">list of objects</param>
        /// <exception cref="ArchPM.NetCore.Exceptions.ValidationException">The list can't be empty</exception>
        public static void NotEmpty<T>(this IList<T> objects)
        {
            if (objects == null || objects.Count == 0)
            {
                throw new ValidationException("The list can't be empty");
            }
        }

        /// <summary>
        /// The defined list names
        /// </summary>
        public static List<String> DefinedListNames = new List<string>() {
            nameof(ArrayList), "LinkedList`1", nameof(Queue), "Queue`1", nameof(Stack), "Stack`1",
            "ICollection`1", "ICollection", "IEnumerable", "IEnumerable`1", "Enumerable",
            "IReadOnlyCollection`1", "IReadOnlyList`1", "IList`1","IList","List`1", "WhereSelectListIterator`2" };


        /// <summary>
        /// Determines whether this instance is list.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is list; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsList(this Type type)
        {
            return
                (type.ReflectedType != null && DefinedListNames.Contains(type.ReflectedType.Name)
              || DefinedListNames.Contains(type.Name));
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="projection"></param>
        public static void ModifyEach<T>(this IList<T> source, Func<T, T> projection)
        {
            for (var i = 0; i < source.Count; i++)
            {
                source[i] = projection(source[i]);
            }
        }

        /// <summary>
        /// The defined list names
        /// </summary>
        private static readonly IList<string> DefinedListNames = new List<string>() {
            nameof(ArrayList), "LinkedList`1", nameof(Queue), "Queue`1", nameof(Stack), "Stack`1",
            "ICollection`1", "ICollection", "IEnumerable", "IEnumerable`1", "Enumerable",
            "IReadOnlyCollection`1", "IReadOnlyList`1", "IList`1","IList","List`1", "WhereSelectListIterator`2" };

        /// <summary>
        /// The defined only generic list names
        /// </summary>
        private static readonly IList<string> DefinedOnlyGenericListNames = new List<string>() {
            "ICollection`1", "IEnumerable`1", "IList`1", "List`1"};


        /// <summary>
        /// Determines whether this instance is list.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is list; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsList(this Type type)
        {
            return
                (type.ReflectedType != null && DefinedListNames.Contains(type.ReflectedType.Name)
              || DefinedListNames.Contains(type.Name));
        }

        /// <summary>
        /// Determines whether [is only generic list].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if [is only generic list] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOnlyGenericList(this Type type)
        {
            return
                (type.ReflectedType != null && DefinedOnlyGenericListNames.Contains(type.ReflectedType.Name)
              || DefinedListNames.Contains(type.Name));
        }
    }
}

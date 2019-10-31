using System;

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CheckExtensions
    {

        /// <summary>
        /// Ifs the null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        public static void IfNull<T>(this T obj, Action<T> action)
        {
            if (obj == null)
            {
                action?.Invoke(default(T));
            }
        }

        /// <summary>
        /// Ifs the not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        public static void IfNotNull<T>(this T obj, Action<T> action)
        {
            if (obj != null)
            {
                action?.Invoke(obj);
            }
        }

    }
}

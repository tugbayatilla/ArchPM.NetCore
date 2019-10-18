using System;
using System.Collections.Generic;

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionExtensions
    {

        /// <summary>
        /// Throws the exception if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="exception">The exception.</param>
        /// <exception cref="Exception"></exception>
        public static void ThrowExceptionIf<T>(this T obj, Func<T, bool> predicate, Exception exception = null)
        {
            if (exception == null)
            {
                exception = new Exception($"An object '{nameof(obj)}' instance can't be null");
            }
            if (predicate == null)
            {
                throw new Exception($"ThrowExceptionIf ExtensionMethod first parameter {nameof(predicate)} is null!");
            }
            if (predicate.Invoke(obj))
            {
                throw exception;
            }
        }

        /// <summary>
        /// Throws the exception if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="Exception"></exception>
        public static void ThrowExceptionIf<T>(this T obj, Func<T, bool> predicate, string message)
        {
            ThrowExceptionIf(obj, predicate, new Exception(message));
        }

        /// <summary>
        /// Throws the exception if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="message">The message.</param>
        public static void ThrowExceptionIfNull<T>(this object obj, string message = "") where T : Exception
        {
            if (string.IsNullOrEmpty(message))
            {
                message = "";
            }
            var ex = (T)Activator.CreateInstance(typeof(T), message);

            if (obj is string)
            {
                ThrowExceptionIf(obj, p => string.IsNullOrEmpty(obj as string), ex);
            }
            else
            {
                ThrowExceptionIf(obj, p => p == null, ex);
            }
        }

        /// <summary>
        /// Gets all exception messages
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public static string GetAllMessages(this Exception ex, Func<Exception, string> predicate = null)
        {
            var message = string.Empty;

            if (ex == null)
            {
                return message;
            }

            message = predicate == null ? $"[{ex.GetType().Name}]:{ex.Message}\r\n" : predicate.Invoke(ex);

            if (ex.InnerException != null)
            {
                message += GetAllMessages(ex.InnerException, predicate);
            }

            return message;
        }

        /// <summary>
        /// Gets all exceptions.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static IEnumerable<Exception> GetAllExceptions(this Exception ex)
        {
            while (true)
            {
                if (ex != null)
                {
                    yield return ex;
                }

                if (ex?.InnerException == null)
                {
                    yield break;
                }

                ex = ex.InnerException;
            }
        }
    }
}

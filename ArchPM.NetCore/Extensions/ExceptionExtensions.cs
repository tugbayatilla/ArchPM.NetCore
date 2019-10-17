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
        public static void ThrowExceptionIf<T>(this T obj, Func<T, Boolean> predicate, Exception exception = null)
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
        public static void ThrowExceptionIf<T>(this T obj, Func<T, Boolean> predicate, String message)
        {
            ThrowExceptionIf(obj, predicate, new Exception(message));
        }

        ///// <summary>
        ///// Throws the exception if null.
        ///// </summary>
        ///// <param name="obj">The object.</param>
        ///// <param name="exception">The exception.</param>
        //public static void ThrowExceptionIfNull(this Object obj, Exception exception = null)
        //{
        //    obj.ThrowExceptionIf(p => p == null, exception);
        //}

        ///// <summary>
        ///// Throws the exception if null.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj">The object.</param>
        ///// <param name="message">The message.</param>
        //public static void ThrowExceptionIfNull<T>(this T obj, String message)
        //{
        //    obj.ThrowExceptionIf(p => p == null, new Exception(message));
        //}

        /// <summary>
        /// Throws the exception if null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="message">The message.</param>
        public static void ThrowExceptionIfNull<T>(this Object obj, String message = "") where T : Exception
        {
            if (String.IsNullOrEmpty(message))
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
        /// Gets all exception messages seperated by \r\n
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="showMessageTypeAsHeader">if set to <c>true</c> [show message type as header].</param>
        /// <param name="messageSeperator"></param>
        /// <returns></returns>
        public static String GetAllMessages(this Exception ex, Boolean showMessageTypeAsHeader = true, String messageSeperator = "\r\n")
        {
            String message = "";
            if (showMessageTypeAsHeader)
                message = String.Format("[{0}]:", ex.GetType().Name);

            message += ex.Message;

            if (ex.InnerException != null)
                message += String.Concat(messageSeperator, GetAllMessages(ex.InnerException, showMessageTypeAsHeader));

            return message;
        }

        /// <summary>
        /// Gets all exceptions.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static IEnumerable<Exception> GetAllExceptions(this Exception ex)
        {
            if (ex != null)
                yield return ex;
            if (ex.InnerException != null)
            {
                foreach (var item in GetAllExceptions(ex.InnerException))
                {
                    yield return item;
                }

            }

            yield break;
        }
    }
}

using ArchPM.NetCore.Enums;
using System;
using System.Collections.Generic;

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string GetDescription(this Enum obj)
        {
            string result = string.Empty;
            if (obj != null)
            {
                Type type = obj.GetType();
                string name = obj.ToString();
                result = EnumManager.GetEnumDescription(type, name);
            }
            return result;
        }

        /// <summary>
        /// Get value as string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetValueAsString(this Enum obj)
        {
            Type type = obj.GetType();

            return Convert.ToString((Int32)type.GetField(obj.ToString()).GetValue(obj));
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string GetName(this Enum obj)
        {
            return Enum.GetName(obj.GetType(), obj);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static Int32 GetValue(this Enum obj)
        {
            return Convert.ToInt32(obj);
        }


        /// <summary>
        /// Iterates all enum items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        public static void Foreach<T>(this Enum obj, Action<T> action)
        {
            var enumNames = Enum.GetNames(typeof(T));

            foreach (var enumName in enumNames)
            {
                T enumRole = (T)Enum.Parse(typeof(T), enumName);
                action(enumRole);
            }
        }

        /// <summary>
        /// To the array.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static IEnumerable<Enum> ToArray(this Enum obj)
        {
            foreach (Enum value in Enum.GetValues(obj.GetType()))
                if (obj.HasFlag(value))
                    yield return value;
        }

        /// <summary>
        /// Determines whether [has] [the specified value].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool Has<T>(this System.Enum type, T value)
        {
            try
            {
                return (((Int32)(object)type & (Int32)(object)value) == (Int32)(object)value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is] [the specified value].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Is<T>(this System.Enum type, T value)
        {
            try
            {
                return (Int32)(object)type == (Int32)(object)value;
            }
            catch
            {
                return false;
            }
        }
    }
}

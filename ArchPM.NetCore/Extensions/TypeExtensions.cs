using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        public static object GetDefault(this Type t)
        {
            Func<object> f = GetDefault<object>;
            return f.Method.GetGenericMethodDefinition().MakeGenericMethod(t).Invoke(null, null);
        }

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T GetDefault<T>()
        {
            return default(T);
        }

        /// <summary>
        /// Gets the constants.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IEnumerable<FieldInfo> GetConstants(this Type type)
        {
            var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }

        /// <summary>
        /// Determines whether [is nullable type] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if [is nullable type] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        /// <summary>
        /// Gets the constants values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetConstantsValues<T>(this Type type) where T : class
        {
            var fieldInfos = GetConstants(type);

            return fieldInfos.Select(fi => fi.GetRawConstantValue() as T);
        }

        /// <summary>
        /// Create and instance and cast
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="constructorArguments"></param>
        /// <returns></returns>
        public static T CreateInstanceAndCast<T>(this Type type, params Object[] constructorArguments)
        {
            if (type.ContainsGenericParameters)
            {
                type = type.MakeGenericType(typeof(T).GenericTypeArguments);
            }
            var instance = (T)Activator.CreateInstance(type, constructorArguments);
            return instance;
        }

        /// <summary>
        /// Recursivlies the type of the check base.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="checkType">Type of the check.</param>
        /// <returns></returns>
        internal static Boolean RecursivlyCheckBaseType(Type type, Type checkType)
        {
            if (type != null)
            {
                if (type == checkType)
                {
                    return true;
                }
                else
                {
                    return RecursivlyCheckBaseType(type.BaseType, checkType);
                }
            }
            else
            {
                return false;
            }
        }

    }
}

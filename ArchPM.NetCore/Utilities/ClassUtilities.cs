using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArchPM.NetCore.Builders;
using ArchPM.NetCore.Extensions;

namespace ArchPM.NetCore.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class ClassUtilities
    {
        /// <summary>
        /// Gets the name of the item by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static ClassItem GetItemByName<T>(string name) where T : class
        {
            var instance = ObjectBuilder.CreateInstance(typeof(T)) as T;
            instance.ThrowExceptionIfNull<NullReferenceException>(nameof(instance));

            return instance.GetItemByName(name);
        }

        /// <summary>
        /// Gets the name of the item by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static ClassItem GetItemByName<T>(this T entity, string name) where T : class
        {
            return entity.GetItems().FirstOrDefault(p => p.Name == name);
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static List<ClassItem> GetItems<T>(this T entity) where T : class
        {
            entity.ThrowExceptionIfNull<ArgumentNullException>(nameof(entity));

            var result = new List<ClassItem>();
            var type = typeof(T);

            var fields = type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly);

            fields.ForEach(field =>
            {
                var item = new ClassItem()
                {
                    Name = field.Name,
                    ValueType = field.FieldType,
                    Value = field.GetRawConstantValue(),
                    ItemType = ClassItemType.Constant,
                    Nullable = IsValueType(field.FieldType)
                };
                result.Add(item);
            });

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead);
            properties.ForEach(property =>
            {
                var item = new ClassItem()
                {
                    Name = property.Name,
                    ValueType = property.PropertyType,
                    Value = property.GetValue(entity, null),
                    ItemType = ClassItemType.Property,
                    Nullable = IsValueType(property.PropertyType)
                };

                result.Add(item);
            });

            return result;
        }

        /// <summary>
        /// Determines whether [is dot net primitive] [the specified accept nullable types].
        /// </summary>
        /// <param name="systemType">Type of the system.</param>
        /// <param name="acceptNullableTypes">if set to <c>true</c> [accept nullable types].</param>
        /// <returns>
        ///   <c>true</c> if [is dot net primitive] [the specified accept nullable types]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValueType(this Type systemType, bool acceptNullableTypes = true)
        {
            if (acceptNullableTypes && systemType.Name == "Nullable`1")
            {
                var nullableSystemType = systemType.GetGenericArguments()[0];
                return IsValueType(nullableSystemType, false);
            }

            return systemType.IsValueType;
        }

    }
}

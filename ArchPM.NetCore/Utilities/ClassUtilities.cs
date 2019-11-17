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
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static ClassItem GetItemByName<TEntity>(string name) where TEntity : class
        {
            var instance = ObjectBuilder.CreateInstance(typeof(TEntity)) as TEntity;
            instance.ThrowExceptionIfNull<NullReferenceException>(nameof(instance));

            return instance.GetItemByName(name);
        }

        /// <summary>
        /// Gets the name of the item by.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static ClassItem GetItemByName<TEntity>(this TEntity entity, string name) where TEntity : class
        {
            return entity.GetItems().FirstOrDefault(p => p.Name == name);
        }

        /// <summary>
        /// Gets the name of the item by.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static ClassItem<TFilter> GetItemByName<TEntity, TFilter>(this TEntity entity, string name) where TEntity : class
        {
            return entity.GetItems<TEntity,TFilter>().FirstOrDefault(p => p.Name == name);
        }


        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="filterTypes">The filter types.</param>
        /// <returns></returns>
        public static List<ClassItem> GetItems<TEntity>(this TEntity entity, params Type[] filterTypes) where TEntity : class
        {
            var result = new List<ClassItem>();
            var type = typeof(TEntity);

            var fields = type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly);

            if (filterTypes.Any())
            {
                fields = fields.Where(p => filterTypes.Contains(p.FieldType));
            }

            fields.ForEach(field =>
            {
                var item = new ClassItem()
                {
                    Name = field.Name,
                    ValueType = field.FieldType,
                    Value = field.GetRawConstantValue(),
                    ItemType = ClassItemType.Constant,
                    Nullable = field.FieldType.IsValueType()
                };
                result.Add(item);
            });

            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead);
            if (filterTypes.Any())
            {
                properties = properties.Where(p => filterTypes.Contains(p.PropertyType));
            }

            properties.ForEach(property =>
            {
                var item = new ClassItem()
                {
                    Name = property.Name,
                    ValueType = property.PropertyType,
                    Value = entity != null ? property.GetValue(entity, null) : null,
                    ItemType = ClassItemType.Property,
                    Nullable = property.PropertyType.IsValueType()
                };

                result.Add(item);
            });

            return result;
        }


        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static List<ClassItem<TFilter>> GetItems<TEntity, TFilter>(this TEntity entity) where TEntity : class
        {
            var result = new List<ClassItem<TFilter>>();

            var classItems = GetItems(entity, typeof(TFilter));
            foreach (var classItem in classItems)
            {
                result.Add(new ClassItem<TFilter>()
                {
                    Nullable = classItem.Nullable,
                    Name = classItem.Name,
                    Value = (TFilter)classItem.Value,
                    ItemType = classItem.ItemType,
                    ValueType = classItem.ValueType
                });
            }

            return result;
        }

    }
}

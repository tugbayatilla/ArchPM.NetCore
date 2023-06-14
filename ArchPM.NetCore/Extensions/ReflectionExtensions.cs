using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Collects the properties.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entityType</exception>
        public static IEnumerable<EntityPropertyInfo> ToEntityPropertyInfos(this Type entityType, Func<EntityPropertyInfo, bool> predicate = null)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (entityType.Name == "Void")
            {
                return new List<EntityPropertyInfo>();
            }

            if (entityType.Module.Name == "mscorlib.dll")
            {
                return new List<EntityPropertyInfo>();
            }

            try
            {
                var entity = Activator.CreateInstance(entityType);
                return ToEntityPropertyInfos(entity, predicate);
            }
            catch
            {
                return new List<EntityPropertyInfo>();
            }
        }

        /// <summary>
        /// Collects the properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entity</exception>
        public static IEnumerable<EntityPropertyInfo> ToEntityPropertyInfos<T>(this T entity, Func<EntityPropertyInfo, bool> predicate = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var entityProperty = entity.ToEntityPropertyInfo(property);
                entityProperty.Attributes = property.GetCustomAttributes();

                if (predicate != null)
                {
                    if (predicate(entityProperty))
                    {
                        yield return entityProperty;
                    }
                }
                else
                {
                    yield return entityProperty;
                }

            }
        }

        public static bool IsDotNetPirimitive(this Type systemType, bool acceptNullable = true)
        {
            if (acceptNullable)
            {
                if (systemType.Name == "IsNullable`1")
                {
                    var nullableSystemType = systemType.GetGenericArguments()[0];
                    return IsDotNetPirimitive(nullableSystemType, false);
                }
            }

            if (systemType == typeof(string)
                || systemType == typeof(char)
                || systemType == typeof(byte)
                || systemType == typeof(int)
                || systemType == typeof(long)
                || systemType == typeof(short)
                || systemType == typeof(float)
                || systemType == typeof(long)
                || systemType == typeof(short)
                || systemType == typeof(double)
                || systemType == typeof(decimal)
                || systemType == typeof(DateTime)
                || systemType == typeof(bool)
                || systemType == typeof(Guid)
                || systemType == typeof(Enum)
                || systemType == typeof(uint)
                || systemType == typeof(ulong)
                || systemType == typeof(ushort)
                || systemType == typeof(sbyte)
                || IsEnumOrIsBaseEnum(systemType))
            {


                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsGenericNullable(this PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsGenericType 
                   && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

       

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">Name of the propertyInfo.</param>
        /// <param name="propertyVal">The propertyInfo value.</param>
        /// <param name="applyChangeType">if set to <c>true</c> [change type]. if you explicitly change the type of the propertyInfo then set as false</param>
        public static void SetValue<T>(this T obj, string propertyName, object propertyVal, bool applyChangeType = true)
        {
            //find out the type
            var type = obj.GetType();

            //get the propertyInfo information based on the type
            var propertyInfo = type.GetProperty(propertyName);

            //Convert.ChangeType does not handle conversion to nullable types
            //if the propertyInfo type is nullable, we need to get the underlying type of the propertyInfo
            if (propertyInfo != null)
            {
                var targetType = propertyInfo.PropertyType.IsNullableType() ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;

                if (applyChangeType)
                {
                    //Returns an System.Object with the specified System.Type and whose value is
                    //equivalent to the specified object.
                    propertyVal = Convert.ChangeType(propertyVal, targetType ?? throw new InvalidOperationException());
                }
            }

            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                //Set the value of the propertyInfo
                propertyInfo.SetValue(obj, propertyVal, null);
            }

        }

        private static EntityPropertyInfo ToEntityPropertyInfo<T>(this T entity, PropertyInfo property)
        {
            var entityProperty = new EntityPropertyInfo
            {
                Name = property.Name
            };
            try
            {
                entityProperty.Value = property.GetValue(entity, null);
            }
            catch
            {
                entityProperty.Value = null;
            }
            entityProperty.ValueTypeName = property.PropertyType.Name;
            entityProperty.ValueType = property.PropertyType;
            entityProperty.IsNullable = false;

            if (property.IsGenericNullable())
            {
                entityProperty.ValueTypeName = Nullable.GetUnderlyingType(property.PropertyType)?.Name;
                entityProperty.ValueType = Nullable.GetUnderlyingType(property.PropertyType);
                entityProperty.IsNullable = true;
            }

            //when datetime gets the default value
            if (entityProperty.Value != null && entityProperty.ValueType == typeof(DateTime) && (DateTime)entityProperty.Value == default(DateTime))
            {
                entityProperty.Value = null;
                entityProperty.IsNullable = true;
            }
            //string is reference type, it can be null
            if (entityProperty.ValueType == typeof(string))
            {
                entityProperty.IsNullable = true;
            }
            entityProperty.IsPrimitive = entityProperty.ValueType.IsDotNetPirimitive();
            entityProperty.IsEnum = IsEnumOrIsBaseEnum(entityProperty.ValueType);
            entityProperty.IsList = entityProperty.ValueType.IsList();
            entityProperty.IsArray = entityProperty.ValueType?.IsArray ?? false;
            if (entityProperty.IsList)
            {
                entityProperty.IsNullable = true;
            }
            if (!property.IsGenericNullable() && !entityProperty.IsPrimitive)
            {
                entityProperty.IsNullable = true;
            }
            if (entity.GetType().IsClass && !entityProperty.IsPrimitive)
            {
                entityProperty.IsClass = true;
            }
            return entityProperty;
        }

        /// <summary>
        /// Determines whether [is enum or is base enum] [the specified type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if [is enum or is base enum] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEnumOrIsBaseEnum(this Type type)
        {
            return type.IsEnum || (type.BaseType != null && type.BaseType == typeof(Enum));
        }

    }
}

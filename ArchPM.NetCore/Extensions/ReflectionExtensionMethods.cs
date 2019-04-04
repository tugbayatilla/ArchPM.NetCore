using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ReflectionExtensionMethods
    {
        /// <summary>
        /// Collects the properties.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entityType</exception>
        public static IEnumerable<PropertyDTO> CollectProperties(this Type entityType, Func<PropertyDTO, Boolean> predicate = null)
        {
            if (entityType == null)
                throw new ArgumentNullException("entityType");
            if (entityType.Name == "Void")
                return new List<PropertyDTO>();
            if (entityType.Module.Name == "mscorlib.dll")
                return new List<PropertyDTO>();

            Object entity = null;
            try
            {
                entity = Activator.CreateInstance(entityType);
                return CollectProperties(entity, predicate);
            }
            catch {
                return new List<PropertyDTO>();
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
        public static IEnumerable<PropertyDTO> CollectProperties<T>(this T entity, Func<PropertyDTO, Boolean> predicate = null)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            PropertyInfo[] properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var entityProperty = entity.ConvertPropertyInfoToPropertyDTO(property);
                entityProperty.Attributes = property.GetCustomAttributes();

                if (predicate != null)
                {
                    if (predicate(entityProperty))
                    {
                        yield return entityProperty;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    yield return entityProperty;
                }

            }
        }

        /// <summary>
        /// Determines whether [is dot net pirimitive].
        /// </summary>
        /// <param name="systemType">Type of the system.</param>
        /// <param name="acceptNullables">if set to <c>true</c> [accept nullables].</param>
        /// <returns>
        ///   <c>true</c> if [is dot net pirimitive] [the specified system type]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsDotNetPirimitive(this Type systemType, Boolean acceptNullables = true)
        {
            if (acceptNullables)
            {
                if (systemType.Name == "Nullable`1")
                {
                    var nullableSystemType = systemType.GetGenericArguments()[0];
                    return IsDotNetPirimitive(nullableSystemType, false);
                }
            }

            if (systemType == typeof(String)
                || systemType == typeof(Char)
                || systemType == typeof(Byte)
                || systemType == typeof(Int32)
                || systemType == typeof(Int64)
                || systemType == typeof(Int16)
                || systemType == typeof(float)
                || systemType == typeof(long)
                || systemType == typeof(short)
                || systemType == typeof(Double)
                || systemType == typeof(Decimal)
                || systemType == typeof(DateTime)
                || systemType == typeof(Boolean)
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

        /// <summary>
        /// Determines whether [is generic nullable].
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>
        ///   <c>true</c> if [is generic nullable] [the specified property]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsGenericNullable(this PropertyInfo property)
        {
            return property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        /// <summary>
        /// Adds the property.
        /// </summary>
        /// <param name="expando">The expando.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        public static void AddProperty(this ExpandoObject expando, String propertyName, Object propertyValue)
        {
            var expandoDict = expando as IDictionary<String, Object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyVal">The property value.</param>
        /// <param name="appyChangeType">if set to <c>true</c> [change type]. if you explicitly change the type of the property then set as false</param>
        public static void SetValue<T>(this T obj, string propertyName, object propertyVal, bool appyChangeType = true)
        {
            //find out the type
            Type type = obj.GetType();

            //get the property information based on the type
            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            //find the property type
            Type propertyType = propertyInfo.PropertyType;

            //Convert.ChangeType does not handle conversion to nullable types
            //if the property type is nullable, we need to get the underlying type of the property
            var targetType = propertyInfo.PropertyType.IsNullableType() ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;

            if (appyChangeType)
            {
                //Returns an System.Object with the specified System.Type and whose value is
                //equivalent to the specified object.
                propertyVal = Convert.ChangeType(propertyVal, targetType);
            }

            if (propertyInfo.CanWrite)
            {
                //Set the value of the property
                propertyInfo.SetValue(obj, propertyVal, null);
            }

        }



        /// <summary>
        /// Converts the property information to property dto.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        internal static PropertyDTO ConvertPropertyInfoToPropertyDTO<T>(this T entity, PropertyInfo property)
        {
            var entityProperty = new PropertyDTO
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
            entityProperty.Nullable = false;

            if (property.IsGenericNullable())
            {
                entityProperty.ValueTypeName = Nullable.GetUnderlyingType(property.PropertyType).Name;
                entityProperty.ValueType = Nullable.GetUnderlyingType(property.PropertyType);
                entityProperty.Nullable = true;
            }

            //when datetime gets the default value
            if (entityProperty.Value != null && entityProperty.ValueType == typeof(DateTime) && (DateTime)entityProperty.Value == default(DateTime))
            {
                entityProperty.Value = null;
                entityProperty.Nullable = true;
            }
            //String is reference type, it can be null
            if (entityProperty.ValueType == typeof(String))
            {
                entityProperty.Nullable = true;
            }
            entityProperty.IsPrimitive = entityProperty.ValueType.IsDotNetPirimitive();
            entityProperty.IsEnum = IsEnumOrIsBaseEnum(entityProperty.ValueType);
            entityProperty.IsList = entityProperty.ValueType.IsList();
            if (entityProperty.IsList)
            {
                entityProperty.Nullable = true;
            }
            if (!property.IsGenericNullable() && !entityProperty.IsPrimitive)
            {
                entityProperty.Nullable = true;
            }
            if(entity.GetType().IsClass && !entityProperty.IsPrimitive)
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
        internal static Boolean IsEnumOrIsBaseEnum(Type type)
        {
            return type.IsEnum || (type.BaseType != null && type.BaseType == typeof(Enum));
        }

    }
}

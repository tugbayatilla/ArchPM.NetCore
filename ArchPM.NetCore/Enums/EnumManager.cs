﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ArchPM.NetCore.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumManager
    {
        /// <summary>
        /// Gets the enum description.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetEnumDescription(Type type, string name)
        {
            string result = name;
            var attributes = type.GetField(name).GetCustomAttributes<EnumDescriptionAttribute>(false).ToList();

            //if no lang, then get first desctiption
            var attribute = attributes.FirstOrDefault();
            if (attribute != null)
            {
                result = attribute.Description;
            }

            return result;
        }
    }

    /// <summary>
    /// T enum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EnumManager<T> : EnumManager
    {
        /// <summary>
        /// Iterates all enum items
        /// </summary>
        /// <param name="action">The action.</param>
        public static void Foreach(Action<T> action)
        {
            var enumNames = Enum.GetNames(typeof(T));

            foreach (var enumName in enumNames)
            {
                T enumRole = (T)Enum.Parse(typeof(T), enumName);
                action(enumRole);
            }
        }


        #region Description

        /// <summary>
        /// Get description if exist, or name
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetDescription(string value)
        {
            var t = Parse(value);

            return GetDescription(t);
        }

        /// <summary>
        /// Get description if exist, or name
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetDescription(T e)
        {
            Type type = typeof(T);
            string name = e.ToString();

            var result = EnumManager.GetEnumDescription(type, name);
            return result;
        }

        /// <summary>
        /// Get description if exist, or name
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(Int32 value)
        {
            Type type = typeof(T);
            string name = System.Enum.GetName(type, value);

            var result = EnumManager.GetEnumDescription(type, name);
            return result;
        }




        #endregion

        #region GetName

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetName(Int32 value)
        {
            return System.Enum.GetName(typeof(T), value);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static string GetName(T e)
        {
            return System.Enum.GetName(typeof(T), e);
        }

        /// <summary>
        /// Gets the value. 
        /// </summary>
        /// <typeparam name="U">Dont use string as Type!</typeparam>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static U GetValue<U>(T e)
        {
            if (typeof(U) == typeof(string))
                throw new Exception("Dont use string as Type! Use GetValueAsString method instead!");

            return (U)typeof(T).GetField(e.ToString()).GetValue(e);
        }

        /// <summary>
        /// Gets the value as string.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static string GetValueAsString(T e)
        {
            return Convert.ToString((Int32)typeof(T).GetField(e.ToString()).GetValue(e));
        }

        #endregion


        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <param name="hasExcluded">if set to <c>true</c> [has excluded].</param>
        /// <returns></returns>
        public static IEnumerable<EnumResult> GetList(bool hasExcluded = false)
        {
            List<EnumResult> result = new List<EnumResult>();

            Type type = typeof(T);
            foreach (string name in Enum.GetNames(type))
            {
                FieldInfo fi = type.GetField(name);

                string desc = string.Empty;
                //can only 1 StringValueAttribute
                var attribute = fi.GetCustomAttributes<EnumDescriptionAttribute>(false).FirstOrDefault();
                //exist and not exluded internally and externally
                if (attribute != null )
                {
                    if ((hasExcluded && attribute.Exclude))
                        continue;

                    desc = attribute.Description;
                }

                result.Add(new EnumResult() { Description = desc, Name = fi.Name, Value = (Int32)fi.GetRawConstantValue() });
            }

            return result;
        }


        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T Parse(string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T Parse(Int32 value)
        {
            return (T)System.Enum.Parse(typeof(T), Convert.ToString(value));
        }

        /// <summary>
        /// Tries the parse.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">If exception orrurs, returns the default value</param>
        /// <returns></returns>
        public static T TryParse(string value, T defaultValue)
        {
            T result = default(T);
            try
            {
                result = (T)System.Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                result = defaultValue;
            }

            return result;
        }

        

    }
}

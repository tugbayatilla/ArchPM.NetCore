using System;
using System.Collections.Generic;
using System.Text;

namespace ArchPM.NetCore.Extensions.ConvertionExtensions
{
    public static class ConvertionExtensionMethods
    {
        /// <summary>
        /// Tries the convert to given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The expression.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T TryToConvert<T>(this Object obj, T defaultValue)
        {
            if (obj == null)
                return defaultValue;

            String nullable = "Nullable`1";

            var fullName = typeof(T).FullName;
            var name = typeof(T).Name;
            Object result = defaultValue;

            try
            {
                if (name == "Decimal" || (name == nullable && fullName.Contains("System.Decimal")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToDecimal(obj.ToString());
                    }
                }
                else if (name == "Double" || (name == nullable && fullName.Contains("System.Decimal")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToDouble(obj.ToString());
                    }
                }
                else if (name == "Int16" || (name == nullable && fullName.Contains("System.Int16")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToInt16(obj.ToString());
                    }
                }
                else if (name == "Int32" || (name == nullable && fullName.Contains("System.Int32")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToInt32(obj.ToString());
                    }
                }
                else if (name == "Int64" || (name == nullable && fullName.Contains("System.Int64")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToInt64(obj.ToString());
                    }
                }
                else if (name == "UInt16" || (name == nullable && fullName.Contains("System.UInt16")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToUInt16(obj.ToString());
                    }
                }
                else if (name == "UInt32" || (name == nullable && fullName.Contains("System.UInt32")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToUInt32(obj.ToString());
                    }
                }
                else if (name == "UInt64" || (name == nullable && fullName.Contains("System.UInt64")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToUInt64(obj.ToString());
                    }
                }
                else if (name == "Single" || (name == nullable && fullName.Contains("System.Single")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToSingle(obj.ToString());
                    }
                }
                else if (name == "Byte" || (name == nullable && fullName.Contains("System.Byte")))
                {
                    Byte.TryParse(obj.ToString(), out byte temp);
                    result = temp;
                }
                else if (name == "DateTime" || (name == nullable && fullName.Contains("System.DateTime")))
                {
                    DateTime.TryParse(obj.ToString(), out DateTime temp);
                    result = temp;
                }
                else if (name == "Boolean" || (name == nullable && fullName.Contains("System.Boolean")))
                {
                    Boolean.TryParse(obj.ToString(), out bool temp);
                    result = temp;
                }
                else if (name == "String" || (name == nullable && fullName.Contains("System.String")))
                {
                    String temp = Convert.ToString(obj);
                    if (!String.IsNullOrEmpty(temp))
                        result = temp;
                }
            }
            catch
            {
                result = defaultValue;
            }

            return (T)result;
        }

        /// <summary>
        /// Tries to convert.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="type">The type.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static Object TryToConvert(this Object obj, Type type, Object defaultValue)
        {
            if (obj == null)
                return defaultValue;

            String nullable = "Nullable`1";

            var fullName = type.FullName;
            var name = type.Name;
            Object result = defaultValue;

            try
            {
                if (name == "Decimal" || (name == nullable && fullName.Contains("System.Decimal")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToDecimal(obj.ToString());
                    }
                }
                else if (name == "Double" || (name == nullable && fullName.Contains("System.Decimal")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToDouble(obj.ToString());
                    }
                }
                else if (name == "Int16" || (name == nullable && fullName.Contains("System.Int16")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToInt16(obj.ToString());
                    }
                }
                else if (name == "Int32" || (name == nullable && fullName.Contains("System.Int32")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToInt32(obj.ToString());
                    }
                }
                else if (name == "Int64" || (name == nullable && fullName.Contains("System.Int64")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToInt64(obj.ToString());
                    }
                }
                else if (name == "UInt16" || (name == nullable && fullName.Contains("System.UInt16")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToUInt16(obj.ToString());
                    }
                }
                else if (name == "UInt32" || (name == nullable && fullName.Contains("System.UInt32")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToUInt32(obj.ToString());
                    }
                }
                else if (name == "UInt64" || (name == nullable && fullName.Contains("System.UInt64")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToUInt64(obj.ToString());
                    }
                }
                else if (name == "Single" || (name == nullable && fullName.Contains("System.Single")))
                {
                    if (IsNumeric(obj))
                    {
                        result = Convert.ToSingle(obj.ToString());
                    }
                }
                else if (name == "Byte" || (name == nullable && fullName.Contains("System.Byte")))
                {
                    Byte.TryParse(obj.ToString(), out byte temp);
                    result = temp;
                }
                else if (name == "DateTime" || (name == nullable && fullName.Contains("System.DateTime")))
                {
                    DateTime.TryParse(obj.ToString(), out DateTime temp);
                    result = temp;
                }
                else if (name == "Boolean" || (name == nullable && fullName.Contains("System.Boolean")))
                {
                    Boolean.TryParse(obj.ToString(), out bool temp);
                    result = temp;
                }
                else if (name == "String" || (name == nullable && fullName.Contains("System.String")))
                {
                    String temp = Convert.ToString(obj);
                    if (!String.IsNullOrEmpty(temp))
                        result = temp;
                }
            }
            catch
            {
                result = defaultValue;
            }

            return result;
        }

        /// <summary>
        /// Tries the convert to given type
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object TryToConvert(this Object obj, Type type)
        {
            Object defaultValue = null;
            if (type.IsValueType)
            {
                defaultValue = Activator.CreateInstance(type);
            }

            return TryToConvert(obj, type, defaultValue);
        }

        /// <summary>
        /// Determines whether the specified expression is numeric.
        /// </summary>
        /// <param name="obj">The expression.</param>
        /// <returns></returns>
        public static Boolean IsNumeric(this Object obj)
        {
            bool isNum = Double.TryParse(Convert.ToString(obj), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out double retNum);
            return isNum;
        }
    }
}

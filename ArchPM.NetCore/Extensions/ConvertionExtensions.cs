using System;

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConvertionExtensions
    {
        /// <summary>
        /// Tries the convert to given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The expression.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T TryToConvert<T>(this object obj, T defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }

            const string nullable = "Nullable`1";

            var fullName = typeof(T).FullName;
            var name = typeof(T).Name;
            object result = defaultValue;

            try
            {
                switch (name)
                {
                    case "Decimal":
                    case nullable when fullName != null && fullName.Contains("System.Decimal"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToDecimal(obj.ToString());
                        }

                        break;
                    }
                    case "Double":
                    case nullable when fullName != null && fullName.Contains("System.Decimal"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToDouble(obj.ToString());
                        }

                        break;
                    }
                    case "Int16":
                    case nullable when fullName != null && fullName.Contains("System.Int16"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToInt16(obj.ToString());
                        }

                        break;
                    }
                    case "Int32":
                    case nullable when fullName != null && fullName.Contains("System.Int32"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToInt32(obj.ToString());
                        }

                        break;
                    }
                    case "Int64":
                    case nullable when fullName != null && fullName.Contains("System.Int64"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToInt64(obj.ToString());
                        }

                        break;
                    }
                    case "UInt16":
                    case nullable when fullName != null && fullName.Contains("System.UInt16"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToUInt16(obj.ToString());
                        }

                        break;
                    }
                    case "UInt32":
                    case nullable when fullName != null && fullName.Contains("System.UInt32"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToUInt32(obj.ToString());
                        }

                        break;
                    }
                    case "UInt64":
                    case nullable when fullName != null && fullName.Contains("System.UInt64"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToUInt64(obj.ToString());
                        }

                        break;
                    }
                    case "Single":
                    case nullable when fullName != null && fullName.Contains("System.Single"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToSingle(obj.ToString());
                        }

                        break;
                    }
                    case "Byte":
                    case nullable when fullName != null && fullName.Contains("System.Byte"):
                    {
                        byte.TryParse(obj.ToString(), out var temp);
                        result = temp;
                        break;
                    }
                    case "DateTime":
                    case nullable when fullName != null && fullName.Contains("System.DateTime"):
                    {
                        DateTime.TryParse(obj.ToString(), out var temp);
                        result = temp;
                        break;
                    }
                    case "bool":
                    case nullable when fullName != null && fullName.Contains("System.Boolean"):
                    {
                        bool.TryParse(obj.ToString(), out var temp);
                        result = temp;
                        break;
                    }
                    case "string":
                    case nullable when fullName != null && fullName.Contains("System.String"):
                    {
                        var temp = Convert.ToString(obj);
                        if (!string.IsNullOrEmpty(temp))
                        {
                            result = temp;
                        }

                        break;
                    }
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
        public static object TryToConvert(this object obj, Type type, object defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }

            const string nullable = "Nullable`1";

            var fullName = type.FullName;
            var name = type.Name;
            object result = defaultValue;

            try
            {
                switch (name)
                {
                    case "Decimal":
                    case nullable when fullName != null && fullName.Contains("System.Decimal"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToDecimal(obj.ToString());
                        }

                        break;
                    }
                    case "Double":
                    case nullable when fullName != null && fullName.Contains("System.Decimal"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToDouble(obj.ToString());
                        }

                        break;
                    }
                    case "Int16":
                    case nullable when fullName != null && fullName.Contains("System.Int16"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToInt16(obj.ToString());
                        }

                        break;
                    }
                    case "Int32":
                    case nullable when fullName != null && fullName.Contains("System.Int32"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToInt32(obj.ToString());
                        }

                        break;
                    }
                    case "Int64":
                    case nullable when fullName != null && fullName.Contains("System.Int64"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToInt64(obj.ToString());
                        }

                        break;
                    }
                    case "UInt16":
                    case nullable when fullName != null && fullName.Contains("System.UInt16"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToUInt16(obj.ToString());
                        }

                        break;
                    }
                    case "UInt32":
                    case nullable when fullName != null && fullName.Contains("System.UInt32"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToUInt32(obj.ToString());
                        }

                        break;
                    }
                    case "UInt64":
                    case nullable when fullName != null && fullName.Contains("System.UInt64"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToUInt64(obj.ToString());
                        }

                        break;
                    }
                    case "Single":
                    case nullable when fullName != null && fullName.Contains("System.Single"):
                    {
                        if (IsNumeric(obj))
                        {
                            result = Convert.ToSingle(obj.ToString());
                        }

                        break;
                    }
                    case "Byte":
                    case nullable when fullName != null && fullName.Contains("System.Byte"):
                    {
                        byte.TryParse(obj.ToString(), out var temp);
                        result = temp;
                        break;
                    }
                    case "DateTime":
                    case nullable when fullName != null && fullName.Contains("System.DateTime"):
                    {
                        DateTime.TryParse(obj.ToString(), out var temp);
                        result = temp;
                        break;
                    }
                    case "bool":
                    case nullable when fullName != null && fullName.Contains("System.Boolean"):
                    {
                        bool.TryParse(obj.ToString(), out var temp);
                        result = temp;
                        break;
                    }
                    case "string":
                    case nullable when fullName != null && fullName.Contains("System.String"):
                    {
                        var temp = Convert.ToString(obj);
                        if (!string.IsNullOrEmpty(temp))
                        {
                            result = temp;
                        }

                        break;
                    }
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
        public static object TryToConvert(this object obj, Type type)
        {
            object defaultValue = null;
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
        public static bool IsNumeric(this object obj)
        {
            var isNum = double.TryParse(Convert.ToString(obj), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out var _);
            return isNum;
        }
    }
}

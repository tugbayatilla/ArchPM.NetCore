using ArchPM.NetCore.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SampleCreator
    {
        /// <summary>
        /// Creates the sample data for given type
        /// Every Property value contains its name as a value such as Surname = "Surname" if it is a string,
        /// Boolean values always will be true, if config says otherwise
        /// everynumric value get value depends on their names. sum every char in the name as ascii
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static T CreateSample<T>(this T obj, SampleConfiguration configuration = null)
            where T : class
        {
            //obj.ThrowExceptionIfNull<ArgumentNullException>(nameof(obj));
            if (configuration == null)
            {
                configuration = new SampleConfiguration();
            }
            if (!configuration.IgnoreRecursion)
            {
                configuration.KeyValueContainer.Add(obj.GetType().FullName);
            }


            var properties = obj.CollectProperties().ToList();
            properties.ModifyEach(p =>
            {
                if (configuration.KeyValueContainer.Contains(p.ValueType.FullName))
                    return p;


                if (p.IsPrimitive)
                {
                    if (p.ValueType == typeof(string))
                    {
                        var name = String.Concat(configuration.AlwaysUsePrefixForStringAs, p.Name, configuration.AlwaysUseSuffixForStringAs);
                        obj.SetValue(p.Name, name);
                    }
                    else if (p.ValueType == typeof(bool))
                    {
                        obj.SetValue(p.Name, configuration.AlwaysUseBooleanAs);
                    }
                    else if (p.ValueType == typeof(DateTime))
                    {
                        var dateValue = configuration.AlwaysUseDateTimeAs.AddDays(GenerateValueFromName(p.Name));
                        obj.SetValue(p.Name, dateValue);
                    }
                    else if (p.ValueType == typeof(byte))
                    {
                        var byteValue = GenerateValueFromName(p.Name) / 128;
                        obj.SetValue(p.Name, byteValue);
                    }
                    else
                    {
                        var length = configuration.AlwaysUseNumericPropertiesNameLengthAsValue ? GenerateValueFromName(p.Name) : 0;
                        obj.SetValue(p.Name, length);
                    }
                }
                else if (p.IsList)
                {
                    var ins = Creator.CreateInstance(p.ValueType);
                    if (p.ValueType.IsGenericType)
                    {
                        if (ins is IList)
                        {
                            //findout generic T type
                            var genericType = p.ValueType.GetGenericArguments()[0];

                            for (int i = 0; i < 2; i++)
                            {
                                //create new instance of T
                                var genericTypeInstance = Creator.CreateInstance(genericType);
                                configuration.IgnoreRecursion = true;
                                genericTypeInstance = genericTypeInstance.CreateSample(configuration);

                                if(genericTypeInstance is string)
                                {
                                    genericTypeInstance = $"string_{i}";
                                }

                                //add into the T
                                ins.GetType().GetMethod("Add").Invoke(ins, new[] { genericTypeInstance });
                            }
                        }
                    }
                    obj.SetValue(p.Name, ins);
                }
                else if (p.ValueTypeName == "Dictionary`2")
                {
                    throw new NotImplementedException();
                }
                else if (p.IsClass && !p.IsList)
                {
                    var ins = Creator.CreateInstance(p.ValueType);
                    if (ins != null)
                    {
                        configuration.IgnoreRecursion = false;
                        ins = ins.CreateSample(configuration);
                    }

                    obj.SetValue(p.Name, ins);
                }

                return p;
            });

            return obj as T;
        }

        /// <summary>
        /// Creates and instance and fills it with the with data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static T CreateSample<T>(SampleConfiguration configuration = null) where T : class
        {
            var obj = (T)Creator.CreateInstance(typeof(T));
            return obj.CreateSample(configuration);
        }

        /// <summary>
        /// Generates the name of the value from.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static int GenerateValueFromName(String name)
        {
            int result = 0;
            foreach (var item in name)
            {
                result += (int)item;
            }
            return result;
        }

        /// <summary>
        /// Configuration object for CraeteSampleData
        /// </summary>
        public class SampleConfiguration
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SampleConfiguration"/> class.
            /// </summary>
            public SampleConfiguration()
            {
                AlwaysUseBooleanAs = true;
                AlwaysUseDateTimeAs = DateTime.Now;
                AlwaysUseNumericPropertiesNameLengthAsValue = true;
            }
            /// <summary>
            /// Gets or sets a value indicating whether [boolean values always].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [boolean values always]; otherwise, <c>false</c>.
            /// </value>
            public bool AlwaysUseBooleanAs { get; set; }
            /// <summary>
            /// Gets or sets the prefix for string values.
            /// </summary>
            /// <value>
            /// The prefix for string values.
            /// </value>
            public string AlwaysUsePrefixForStringAs { get; set; }
            /// <summary>
            /// Gets or sets the suffix for string values.
            /// </summary>
            /// <value>
            /// The suffix for string values.
            /// </value>
            public string AlwaysUseSuffixForStringAs { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [always use numeric properties name length asvalue].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [always use numeric properties name length asvalue]; otherwise, <c>false</c>.
            /// </value>
            public bool AlwaysUseNumericPropertiesNameLengthAsValue { get; set; }

            /// <summary>
            /// Gets or sets the use date time as.
            /// </summary>
            /// <value>
            /// The use date time as.
            /// </value>
            public DateTime AlwaysUseDateTimeAs { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [ignore recursion].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [ignore recursion]; otherwise, <c>false</c>.
            /// </value>
            public bool IgnoreRecursion { get; set; } = false;

            internal List<string> KeyValueContainer = new List<string>();
        }
    }
}

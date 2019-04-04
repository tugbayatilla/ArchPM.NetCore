using ArchPM.NetCore.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ArchPM.NetCore.Creators
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
                    else if (p.IsEnum)
                    {
                        obj.SetValue(p.Name, 0, false);
                    }
                    else if (p.ValueType == typeof(bool))
                    {
                        obj.SetValue(p.Name, configuration.AlwaysUseBooleanAs);
                    }
                    else if (p.ValueType == typeof(DateTime))
                    {
                        DateTime dateValue;
                        switch (configuration.DateTimeAddition)
                        {
                            case SampleDateTimeAdditions.AddDays:
                                dateValue = configuration.AlwaysUseDateTimeAs.AddDays(GenerateValueFromName(p.Name));
                                break;
                            case SampleDateTimeAdditions.AddMinutes:
                                dateValue = configuration.AlwaysUseDateTimeAs.AddMinutes(GenerateValueFromName(p.Name));
                                break;
                            case SampleDateTimeAdditions.AddSeconds:
                                dateValue = configuration.AlwaysUseDateTimeAs.AddSeconds(GenerateValueFromName(p.Name));
                                break;
                            default:
                                dateValue = DateTime.Now;
                                break;
                        }
                        obj.SetValue(p.Name, dateValue);
                    }
                    else if (p.ValueType == typeof(byte))
                    {
                        var byteValue = GenerateValueFromName(p.Name) / 128;
                        obj.SetValue(p.Name, byteValue);
                    }
                    else if (p.ValueType == typeof(Guid))
                    {
                        var guidValue = Guid.Parse("00000000-0000-0000-0000-000000000001");
                        obj.SetValue(p.Name, guidValue);
                    }
                    else
                    {
                        var length = configuration.AlwaysUseNumericPropertiesNameLengthAsValue ? GenerateValueFromName(p.Name) : 0;
                        obj.SetValue(p.Name, length);
                    }
                }
                else if (p.IsList)
                {
                    //this section for interface properties.
                    //converting them to List<> generic class. changeType variable is required for setValue.
                    bool changeType = true;
                    if (p.ValueType.IsInterface && p.ValueType.IsOnlyGenericList())
                    {
                        p.ValueType = typeof(List<>).MakeGenericType(p.ValueType.GenericTypeArguments);
                        changeType = false;
                    }

                    var ins = ObjectCreator.CreateInstance(p.ValueType);

                    if (p.ValueType.IsGenericType)
                    {
                        if (ins is IList)
                        {
                            //findout generic T type
                            var genericType = p.ValueType.GetGenericArguments()[0];

                            for (int i = 0; i < 2; i++)
                            {
                                //create new instance of T
                                var genericTypeInstance = ObjectCreator.CreateInstance(genericType);
                                configuration.IgnoreRecursion = true;
                                genericTypeInstance = genericTypeInstance.CreateSample(configuration);

                                if (genericTypeInstance is string)
                                {
                                    genericTypeInstance = $"string_{i}";
                                }

                                //add into the T
                                ins.GetType().GetMethod("Add").Invoke(ins, new[] { genericTypeInstance });
                            }
                        }
                    }
                    obj.SetValue(p.Name, ins, changeType);
                }
                else if (p.ValueTypeName == "Dictionary`2")
                {
                    throw new NotImplementedException();
                }
                else if (p.IsClass && !p.IsList)
                {
                    var ins = ObjectCreator.CreateInstance(p.ValueType);
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
            var obj = (T)ObjectCreator.CreateInstance(typeof(T));
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


    }
}

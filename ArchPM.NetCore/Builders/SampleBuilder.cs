﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArchPM.NetCore.Extensions;
using ArchPM.NetCore.Utilities;

namespace ArchPM.NetCore.Builders
{
    /// <summary>
    /// Creates samples
    /// </summary>
    public static class SampleBuilder
    {
        /// <summary>
        /// Creates the sample.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="configureSampleAction">The configure sample action.</param>
        /// <param name="configureBuilderAction">The configure builder action.</param>
        /// <returns></returns>
        public static T CreateSample<T>(this T obj, Action<T> configureSampleAction = null, Action<SampleBuilderConfiguration> configureBuilderAction = null) where T : class
        {
            var result = SampleBuilder<T>.Init(obj).ConfigureSample(configureSampleAction).ConfigureBuilder(configureBuilderAction).Build().Result;
            return result;
        }

        /// <summary>
        /// Creates the sample.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public static T CreateSample<T>(this T obj, SampleBuilderConfiguration config = null) where T : class
        {
            var result = SampleBuilder<T>.Init(obj).SetConfiguration(config).ConfigureSample().Build().Result;
            return result;
        }

        /// <summary>
        /// Creates the sample.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>() where T : class
        {
            var result = SampleBuilder<T>.Init().ConfigureSample().ConfigureBuilder().Build().Result;
            return result;
        }

        /// <summary>
        /// Creates the sample.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configureSampleAction">The configure sample action.</param>
        /// <param name="configureBuilderAction">The configure builder action.</param>
        /// <returns></returns>
        public static T Create<T>(Action<T> configureSampleAction, Action<SampleBuilderConfiguration> configureBuilderAction = null) where T : class
        {
            var result = SampleBuilder<T>.Init().ConfigureSample(configureSampleAction).ConfigureBuilder(configureBuilderAction).Build().Result;
            return result;
        }

        /// <summary>
        /// Creates the specified configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public static T Create<T>(SampleBuilderConfiguration config) where T : class
        {
            var result = SampleBuilder<T>.Init().SetConfiguration(config).Build().Result;
            return result;
        }

        /// <summary>
        /// Creates the specified configure sample action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configureSampleAction">The configure sample action.</param>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public static T Create<T>(Action<T> configureSampleAction, SampleBuilderConfiguration config) where T : class
        {
            var result = SampleBuilder<T>.Init().SetConfiguration(config).ConfigureSample(configureSampleAction).Build().Result;
            return result;
        }

    }

    /// <summary>
    /// Sample create builder object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SampleBuilder<T> where T : class
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public SampleBuilderConfiguration Configuration { get; private set; } = new SampleBuilderConfiguration();
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public T Result { get; }

        /// <summary>
        /// The configure builder action instance
        /// </summary>
        private Action<SampleBuilderConfiguration> _configureBuilderActionInstance;
        /// <summary>
        /// The configure sample action instance
        /// </summary>
        private Action<T> _configureSampleActionInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBuilder{T}"/> class.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="configuration">The configuration.</param>
        public SampleBuilder(T obj = null, SampleBuilderConfiguration configuration = null)
        {
            Result = obj ?? (T)ObjectBuilder.CreateInstance(typeof(T));
            SetConfiguration(configuration);
        }

        /// <summary>
        /// Initializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static SampleBuilder<T> Init(T obj = null, SampleBuilderConfiguration configuration = null)
        {
            var builder = new SampleBuilder<T>(obj, configuration);
            return builder;
        }

        /// <summary>
        /// Sets the configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public SampleBuilder<T> SetConfiguration(SampleBuilderConfiguration config)
        {
            if (config != null)
            {
                Configuration = config;
            }

            return this;
        }

        /// <summary>
        /// Configures the builder.
        /// </summary>
        /// <param name="configAction">The configuration action.</param>
        /// <returns></returns>
        public SampleBuilder<T> ConfigureBuilder(Action<SampleBuilderConfiguration> configAction = null)
        {
            if (!Configuration.IgnoreRecursion)
            {
                Configuration.KeyValueContainer.Add(Result.GetType().FullName);
            }

            _configureBuilderActionInstance = configAction;

            return this;
        }

        /// <summary>
        /// Configures the sample.
        /// </summary>
        /// <param name="configAction">The configuration action.</param>
        /// <returns></returns>
        public SampleBuilder<T> ConfigureSample(Action<T> configAction = null)
        {
            _configureSampleActionInstance = configAction;

            return this;
        }


        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        public SampleBuilder<T> Build()
        {
            var properties = Result.CollectProperties().ToList();
            //calling Pre Configuration Action
            _configureBuilderActionInstance?.Invoke(Configuration);

            properties.ModifyEach(p =>
            {
                if (Configuration.KeyValueContainer.Contains(p.ValueType.FullName))
                {
                    return p;
                }


                if (p.IsPrimitive)
                {
                    if (p.ValueType == typeof(string))
                    {
                        var name = string.Concat(Configuration.AlwaysUsePrefixForStringAs, p.Name,
                            Configuration.AlwaysUseSuffixForStringAs);
                        ReflectionExtensions.SetValue(Result, p.Name, name);
                    }
                    else if (p.IsEnum)
                    {
                        ReflectionExtensions.SetValue(Result, p.Name, 0, false);
                    }
                    else if (p.ValueType == typeof(bool))
                    {
                        ReflectionExtensions.SetValue(Result, p.Name, Configuration.AlwaysUseBooleanAs);
                    }
                    else if (p.ValueType == typeof(DateTime))
                    {
                        DateTime dateValue;
                        switch (Configuration.AlwaysUseDateTimeAddition)
                        {
                            case SampleDateTimeAdditions.AddDays:
                                dateValue = Configuration.AlwaysUseDateTimeAs.AddDays(GenerateValueFromName(p.Name));
                                break;
                            case SampleDateTimeAdditions.AddMinutes:
                                dateValue = Configuration.AlwaysUseDateTimeAs.AddMinutes(GenerateValueFromName(p.Name));
                                break;
                            case SampleDateTimeAdditions.AddSeconds:
                                dateValue = Configuration.AlwaysUseDateTimeAs.AddSeconds(GenerateValueFromName(p.Name));
                                break;
                            default:
                                dateValue = Configuration.AlwaysUseDateTimeAs;
                                break;
                        }

                        ReflectionExtensions.SetValue(Result, p.Name, dateValue);
                    }
                    else if (p.ValueType == typeof(byte))
                    {
                        var byteValue = GenerateValueFromName(p.Name) / 128;
                        ReflectionExtensions.SetValue(Result, p.Name, byteValue);
                    }
                    else if (p.ValueType == typeof(Guid))
                    {
                        var guidValue = Guid.Parse("00000000-0000-0000-0000-000000000001");
                        ReflectionExtensions.SetValue(Result, p.Name, guidValue);
                    }
                    else
                    {
                        var length = Configuration.AlwaysUseNumericPropertiesNameLengthAsValue
                            ? GenerateValueFromName(p.Name)
                            : 0;
                        ReflectionExtensions.SetValue(Result, p.Name, length);
                    }
                }
                else if (p.IsList)
                {
                    //this section for interface properties.
                    //converting them to List<> generic class. changeType variable is required for setValue.
                    var changeType = true;
                    if (p.ValueType.IsInterface && p.ValueType.IsOnlyGenericList())
                    {
                        p.ValueType = typeof(List<>).MakeGenericType(p.ValueType.GenericTypeArguments);
                        changeType = false;
                    }

                    var ins = ObjectBuilder.CreateInstance(p.ValueType);

                    if (p.ValueType.IsGenericType)
                    {
                        if (ins is IList)
                        {
                            //find out generic T type
                            var genericType = p.ValueType.GetGenericArguments()[0];

                            for (var i = 0; i < Configuration.CollectionCount; i++)
                            {

                                var previouslyCreatedInstance =
                                    Configuration.PreviouslyCreatedInstances.FirstOrDefault(
                                        pci => pci.GetType() == genericType
                                    );

                                if (previouslyCreatedInstance == null)
                                {
                                    //create new instance of T
                                    var genericTypeInstance =
                                        ObjectBuilder.CreateInstance(genericType);
                                    
                                    if (!(genericTypeInstance is string))
                                    {
                                        Configuration.PreviouslyCreatedInstances.Add(
                                            genericTypeInstance
                                        );
                                    }

                                    Configuration.IgnoreRecursion = true;
                                    genericTypeInstance =
                                        genericTypeInstance.CreateSample(Configuration);

                                    if (genericTypeInstance is string)
                                    {
                                        genericTypeInstance = $"string_{i}";
                                    }

                                    //add into the T
                                    ins.GetType().GetMethod("Add")?.Invoke(ins, new[] { genericTypeInstance });
                                }
                                else
                                {
                                    ins.GetType().GetMethod("Add")?.Invoke(ins, new[] { previouslyCreatedInstance });
                                }


                            }
                        }
                    }

                    ReflectionExtensions.SetValue(Result, p.Name, ins, changeType);
                }
                else if (p.ValueTypeName == "Dictionary`2")
                {
                    var ins = ObjectBuilder.CreateInstance(p.ValueType);
                    Configuration.PreviouslyCreatedInstances.Add(ins);
                    ReflectionExtensions.SetValue(Result, p.Name, ins); //todo: must be filled with data!
                }
                else if (p.IsClass && !p.IsList)
                {
                    var ins = ObjectBuilder.CreateInstance(p.ValueType);
                    Configuration.PreviouslyCreatedInstances.Add(ins);
                    if (ins != null)
                    {
                        Configuration.IgnoreRecursion = false;
                        ins = ins.CreateSample(Configuration);
                    }

                    ReflectionExtensions.SetValue(Result, p.Name, ins);
                }

                return p;
            });

            _configureSampleActionInstance?.Invoke(Result);

            return this;
        }


        /// <summary>
        /// Generates the name of the value from.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected int GenerateValueFromName(string name)
        {
            var result = 0;
            foreach (var item in name)
            {
                result += item;
            }

            return result;
        }

    }
}
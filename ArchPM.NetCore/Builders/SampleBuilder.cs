using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArchPM.NetCore.Extensions;

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
        public static T CreateSample<T>(this T obj, Action<T> configureSampleAction = null,
            Action<SampleBuilderConfiguration> configureBuilderAction = null) where T : class
        {
            var result = SampleBuilder<T>.Init(obj).ConfigureSample(configureSampleAction)
                .ConfigureBuilder(configureBuilderAction).Build().Result;
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
        public static T Create<T>(Action<T> configureSampleAction,
            Action<SampleBuilderConfiguration> configureBuilderAction = null) where T : class
        {
            var result = SampleBuilder<T>.Init().ConfigureSample(configureSampleAction)
                .ConfigureBuilder(configureBuilderAction).Build().Result;
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
            var result = SampleBuilder<T>.Init().SetConfiguration(config).ConfigureSample(configureSampleAction).Build()
                .Result;
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
        public SampleBuilderConfiguration Configuration { get; private set; } = new();

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
            _configureBuilderActionInstance?.Invoke(Configuration);

            properties.ModifyEach(p =>
            {
                if (Configuration.KeyValueContainer.Contains(p.ValueType.FullName))
                {
                    return p;
                }

                if (p.IsPrimitive)
                {
                    var value = GetResultIfPrimitive(p.ValueType, p.Name);
                    if (value != default)
                    {
                        Result.SetValue(p.Name, value);
                    }
                }
                else if (p.IsArray)
                {
                    var elementType = p.ValueType.GetElementType();
                    var ins = Activator.CreateInstance(p.ValueType, Configuration.CollectionCount);
                    
                    CreateSamplesIntoCollectionInstance(ins, p.Name, elementType);
                    
                    Result.SetValue(p.Name, ins, false);
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

                    var genericType = p.ValueType.GetGenericArguments()[0];
                    var ins = ObjectBuilder.CreateInstance(p.ValueType);
                    CreateSamplesIntoCollectionInstance(ins, p.Name, genericType);
                    
                    Result.SetValue(p.Name, ins, changeType);
                }
                else if (p.ValueTypeName == "Dictionary`2")
                {
                    var ins = ObjectBuilder.CreateInstance(p.ValueType);
                    Configuration.PreviouslyCreatedInstances.Add(ins);
                    
                    Result.SetValue(p.Name, ins); 
                }
                else if (p.IsClass && !p.IsList && !p.IsArray)
                {
                    var ins = GetResultIfOnlyClass(p.ValueType);
                    
                    Result.SetValue(p.Name, ins);
                }

                return p;
            });

            _configureSampleActionInstance?.Invoke(Result);

            return this;
        }

        private void CreateSamplesIntoCollectionInstance(object instance, string instancePropertyName,
            Type instanceItemType)
        {
            for (var i = 0; i < Configuration.CollectionCount; i++)
            {
                var previouslyCreatedInstance =
                    Configuration.PreviouslyCreatedInstances.FirstOrDefault(pci => pci.GetType() == instanceItemType);

                if (previouslyCreatedInstance == null)
                {
                    var value = GetResultIfPrimitive(instanceItemType, instancePropertyName);
                    if (value != default)
                    {
                        if (value is string)
                        {
                            value += $"_{i}";
                        }

                        AddItemIntoCollection(instance, i, value);
                    }
                    else
                    {
                        var value1 = GetResultIfOnlyClass(instanceItemType);
                        AddItemIntoCollection(instance, i, value1);
                    }
                }
                else
                {
                    AddItemIntoCollection(instance, i, previouslyCreatedInstance);
                }
            }
        }

        private static void AddItemIntoCollection(object instance, int index, object previouslyCreatedInstance)
        {
            switch (instance)
            {
                case Array instanceArray:
                    instanceArray.SetValue(previouslyCreatedInstance, index);
                    break;
                case IList instanceList:
                    instanceList.Add(previouslyCreatedInstance);
                    break;
                case IDictionary instanceDictionary:
                    instanceDictionary.Add(index, previouslyCreatedInstance);
                    break;
            }
        }


        private object GetResultIfOnlyClass(Type propertyValueType)
        {
            var previouslyCreatedInstance =
                Configuration.PreviouslyCreatedInstances.FirstOrDefault(pci => pci.GetType() == propertyValueType);

            if (previouslyCreatedInstance != null)
            {
                return previouslyCreatedInstance;
            }

            var ins = ObjectBuilder.CreateInstance(propertyValueType);
            if (ins == null)
            {
                return null;
            }

            Configuration.PreviouslyCreatedInstances.Add(ins);
            Configuration.IgnoreRecursion = false;
            ins = ins.CreateSample(Configuration);
            return ins;
        }

        private object GetResultIfPrimitive(Type propertyValueType, string propertyName)
        {
            if (!propertyValueType.IsDotNetPirimitive())
            {
                return default;
            }

            if (propertyValueType == typeof(string))
            {
                var name = string.Concat(Configuration.AlwaysUsePrefixForStringAs, propertyName,
                    Configuration.AlwaysUseSuffixForStringAs);
                return name;
            }
            else if (propertyValueType.IsEnumOrIsBaseEnum())
            {
                var tempEnum = Enum.GetValues(propertyValueType);
                if (tempEnum.Length > 0)
                {
                    var firstItemInEnum =
                        tempEnum.GetValue(Configuration.AlwaysPickEnumItemIndex);
                    return firstItemInEnum;
                }
            }
            else if (propertyValueType == typeof(bool))
            {
                return Configuration.AlwaysUseBooleanAs;
            }
            else if (propertyValueType == typeof(DateTime))
            {
                DateTime dateValue;
                switch (Configuration.AlwaysUseDateTimeAddition)
                {
                    case SampleDateTimeAdditions.AddDays:
                        dateValue = Configuration.AlwaysUseDateTimeAs.AddDays(GenerateValueFromName(propertyName));
                        break;
                    case SampleDateTimeAdditions.AddMinutes:
                        dateValue = Configuration.AlwaysUseDateTimeAs.AddMinutes(GenerateValueFromName(propertyName));
                        break;
                    case SampleDateTimeAdditions.AddSeconds:
                        dateValue = Configuration.AlwaysUseDateTimeAs.AddSeconds(GenerateValueFromName(propertyName));
                        break;
                    default:
                        dateValue = Configuration.AlwaysUseDateTimeAs;
                        break;
                }

                return dateValue;
            }
            else if (propertyValueType == typeof(byte))
            {
                var byteValue = GenerateValueFromName(propertyName) / 128;
                return (byte)byteValue;
            }
            else if (propertyValueType == typeof(Guid))
            {
                var guidValue = Guid.Parse("00000000-0000-0000-0000-000000000001");
                return guidValue;
            }
            else
            {
                var length = Configuration.AlwaysUseNumericPropertiesNameLengthAsValue
                    ? GenerateValueFromName(propertyName)
                    : 0;
                return length;
            }

            return default;
        }


        /// <summary>
        /// Generates the name of the value from.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private int GenerateValueFromName(string name)
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
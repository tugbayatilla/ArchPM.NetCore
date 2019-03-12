using System;
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
    public static class ClassExtensionMethods
    {



        /// <summary>
        /// Craetes the sample data.
        /// Every Property value contains its name as a value such as Surname = "Surname" if it is a string,
        /// Boolean values always will be true, if config says otherwise
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static T CreateSampleData<T>(this T obj, CreateSampleDataConfiguration configuration = null)
            where T : class
        {
            obj.ThrowExceptionIfNull<ArgumentNullException>(nameof(obj));

            if (configuration == null)
            {
                configuration = new CreateSampleDataConfiguration();
            }
            configuration.KeyValueContainer.Add(obj.GetType().FullName);


            var properties = obj.CollectProperties().ToList();
            properties.ModifyEach(p =>
            {
                if (configuration.KeyValueContainer.Contains(p.ValueTypeOf.FullName))
                    return p;

                if (p.ValueTypeOf == typeof(String))
                {
                    var name = String.Concat(configuration.AlwaysUsePrefixForStringAs, p.Name, configuration.AlwaysUseSuffixForStringAs);
                    obj.SetValue(p.Name, name);
                }
                if (p.ValueTypeOf == typeof(Boolean))
                {
                    obj.SetValue(p.Name, configuration.AlwaysUseBooleanAs);
                }
                if (p.ValueTypeOf == typeof(DateTime))
                {
                    obj.SetValue(p.Name, configuration.AlwaysUseDateTimeAs);
                }
                if (p.Value.IsNumeric())
                {
                    var length = configuration.AlwaysUseNumericPropertiesNameLengthAsvalue ? p.Name.Length : 0;
                    obj.SetValue(p.Name, length);
                }
                if (!p.IsPrimitive)
                {
                    var ins = Creator.CreateInstance(p.ValueTypeOf);
                    if (ins != null)
                    {
                        if (!ListExtensionMethods.IsList(ins.GetType()))
                        {
                            ins = ins.CreateSampleData(configuration);
                        }
                    }

                    obj.SetValue(p.Name, ins);
                }

                return p;
            });

            return obj as T;
        }


    }

    /// <summary>
    /// Configuration object for CraeteSampleData
    /// </summary>
    public class CreateSampleDataConfiguration
    {
        public CreateSampleDataConfiguration()
        {
            AlwaysUseBooleanAs = true;
            AlwaysUseDateTimeAs = DateTime.Now;
            AlwaysUseNumericPropertiesNameLengthAsvalue = true;
        }
        /// <summary>
        /// Gets or sets a value indicating whether [boolean values always].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [boolean values always]; otherwise, <c>false</c>.
        /// </value>
        public Boolean AlwaysUseBooleanAs { get; set; }
        /// <summary>
        /// Gets or sets the prefix for string values.
        /// </summary>
        /// <value>
        /// The prefix for string values.
        /// </value>
        public String AlwaysUsePrefixForStringAs { get; set; }
        /// <summary>
        /// Gets or sets the suffix for string values.
        /// </summary>
        /// <value>
        /// The suffix for string values.
        /// </value>
        public String AlwaysUseSuffixForStringAs { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [always use numeric properties name length asvalue].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [always use numeric properties name length asvalue]; otherwise, <c>false</c>.
        /// </value>
        public bool AlwaysUseNumericPropertiesNameLengthAsvalue { get; set; }

        /// <summary>
        /// Gets or sets the use date time as.
        /// </summary>
        /// <value>
        /// The use date time as.
        /// </value>
        public DateTime AlwaysUseDateTimeAs { get; set; }

        internal List<string> KeyValueContainer = new List<string>();
    }
}

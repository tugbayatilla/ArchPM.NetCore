using System;
using System.Collections.Generic;
using System.Text;

namespace ArchPM.NetCore.Creators
{
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
        /// Gets or sets the date time addition.
        /// </summary>
        /// <value>
        /// The date time addition.
        /// </value>
        public SampleDateTimeAdditions DateTimeAddition { get; set; } = SampleDateTimeAdditions.AddDays;

        /// <summary>
        /// Gets or sets a value indicating whether [ignore recursion].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ignore recursion]; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreRecursion { get; set; } = false;

        internal List<string> KeyValueContainer = new List<string>();
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SampleDateTimeAdditions
    {
        /// <summary>
        /// The add days
        /// </summary>
        AddDays,
        /// <summary>
        /// The add minutes
        /// </summary>
        AddMinutes,
        /// <summary>
        /// The add seconds
        /// </summary>
        AddSeconds
    }
}

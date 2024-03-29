﻿using System;
using System.Collections.Generic;

namespace ArchPM.NetCore.Builders
{
    /// <summary>
    /// Sample Builder Configuration
    /// </summary>
    public class SampleBuilderConfiguration
    {
        /// <summary>
        /// Gets or sets a value indicating whether [boolean values always].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [boolean values always]; otherwise, <c>false</c>.
        /// </value>
        public bool AlwaysUseBooleanAs { get; set; } = true;

        /// <summary>
        /// Gets or sets the collection count.
        /// </summary>
        /// <value>
        /// The collection count.
        /// </value>
        public int CollectionCount { get; set; } = 2;

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
        ///   <c>true</c> if [always use numeric properties name length as value]; otherwise, <c>false</c>.
        /// </value>
        public bool AlwaysUseNumericPropertiesNameLengthAsValue { get; set; } = true;

        /// <summary>
        /// Gets or sets the use date time as.
        /// </summary>
        /// <value>
        /// The use date time as.
        /// </value>
        public DateTime AlwaysUseDateTimeAs { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the date time addition.
        /// </summary>
        /// <value>
        /// The date time addition.
        /// </value>
        public SampleDateTimeAdditions AlwaysUseDateTimeAddition { get; set; } = SampleDateTimeAdditions.None;

        /// <summary>
        /// Gets or sets the index of the always pick enum item.
        /// </summary>
        /// <value>
        /// The index of the always pick enum item.
        /// </value>
        public int AlwaysPickEnumItemIndex { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [ignore recursion].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ignore recursion]; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreRecursion { get; set; }

        internal readonly List<string> KeyValueContainer = new();

        internal readonly List<object> PreviouslyCreatedInstances = new();
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SampleDateTimeAdditions
    {
        /// <summary>
        /// The none
        /// </summary>
        None,

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
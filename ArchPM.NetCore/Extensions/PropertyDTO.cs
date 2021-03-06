﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
// ReSharper disable All

namespace ArchPM.NetCore.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PropertyDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [XmlElement(IsNullable = true)]
        public object Value { get; internal set; }

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        [XmlAttribute]
        public string ValueTypeName { get; internal set; }

        /// <summary>
        /// Gets the value type of.
        /// </summary>
        /// <value>
        /// The value type of.
        /// </value>
        [XmlIgnore]
        public Type ValueType { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see>
        ///     <cref>PropertyDTO</cref>
        /// </see>
        /// is nullable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if nullable; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute]
        public bool Nullable { get; set; } //there is a reason to make set as public

        /// <summary>
        /// Gets a value indicating whether this <see cref="PropertyDTO" /> is .net primitive type such as string, int, decimal etc.
        /// </summary>
        /// <value>
        ///   <c>true</c> if .net primitive type; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
        public bool IsPrimitive { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is class.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is class; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
        public bool IsClass { get; internal set; }

        /// <summary>
        /// Gets the property whether is enum or not
        /// </summary>
        [XmlIgnore]
        public bool IsEnum { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is list; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
        public bool IsList { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is array.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is array; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
        public bool IsArray { get; internal set; }

        /// <summary>
        /// Gets defined attribute types
        /// </summary>
        [XmlIgnore]
        public IEnumerable<Attribute> Attributes { get; internal set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} [{1}] [{2}]", Name, ValueTypeName, Nullable ? "Nullable" : "");
        }
    }
}

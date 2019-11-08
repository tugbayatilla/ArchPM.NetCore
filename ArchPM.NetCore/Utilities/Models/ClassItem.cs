using System;

// ReSharper disable once CheckNamespace
namespace ArchPM.NetCore.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class ClassItem
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public Type ValueType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see>
        ///     <cref>ClassItem</cref>
        /// </see>
        /// is nullable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if nullable; otherwise, <c>false</c>.
        /// </value>
        public bool Nullable { get; set; }

        /// <summary>
        /// Gets or sets the type of the item.
        /// </summary>
        /// <value>
        /// The type of the item.
        /// </value>
        public ClassItemType ItemType { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format($"{ItemType}:{Name}:{ValueType.Name}");
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public enum ClassItemType
    {
        /// <summary>
        /// The constant
        /// </summary>
        Constant,
        /// <summary>
        /// The property
        /// </summary>
        Property
    }
}

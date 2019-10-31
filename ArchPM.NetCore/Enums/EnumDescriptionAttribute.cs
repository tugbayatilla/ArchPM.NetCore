using System;

namespace ArchPM.NetCore.Enums
{
    /// <summary>
    /// the attribute to get more information from enums
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class EnumDescriptionAttribute : Attribute
    {
        /// <summary>
        /// Gets and Sets description
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets and Sets it is excluded for not
        /// </summary>
        /// <value>
        ///   <c>true</c> if exclude; otherwise, <c>false</c>.
        /// </value>
        public bool Exclude { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public EnumDescriptionAttribute(string description)
        {
            Description = description;
            Exclude = false;
        }

       
    }
}
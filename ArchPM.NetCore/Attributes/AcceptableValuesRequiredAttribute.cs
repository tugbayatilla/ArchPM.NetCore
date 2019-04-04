using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ArchPM.NetCore.Attributes
{
    /// <summary>
    /// Validates the property must only contains given values
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.RequiredAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class AcceptableValuesRequiredAttribute : RequiredAttribute
    {
        /// <summary>
        /// Gets or sets the accepted values.
        /// </summary>
        /// <value>
        /// The accepted values.
        /// </value>
        public object[] AcceptableValues { get; set; } = new object[] { };

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptableValuesRequiredAttribute"/> class.
        /// </summary>
        /// <param name="acceptedvalues">The acceptedvalues.</param>
        public AcceptableValuesRequiredAttribute(params object[] acceptedvalues)
        {
            this.AcceptableValues = acceptedvalues;
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The data field value to validate.</param>
        /// <returns>
        /// true if validation is successful; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            return AcceptableValues.Any(p => object.Equals(value, p));
        }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>
        /// An instance of the formatted error message.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            return $"{name} does not have accepted value. { string.Join(",", AcceptableValues) }";
        }
    }
}

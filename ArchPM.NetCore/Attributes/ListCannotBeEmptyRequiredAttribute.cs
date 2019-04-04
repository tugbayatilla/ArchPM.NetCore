using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ArchPM.NetCore.Attributes
{
    /// <summary>
    /// for IEnumerable properties, validates the collection must have a item
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.RequiredAttribute" />
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ListCannotBeEmptyRequiredAttribute : RequiredAttribute
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The data field value to validate.</param>
        /// <returns>
        /// true if validation is successful; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            var list = value as IEnumerable;
            return list != null && list.GetEnumerator().MoveNext();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArchPM.NetCore.Attributes
{
    /// <summary>
    /// valodator for required attributes
    /// </summary>
    public static class RequiredAttributeValidator
    {
        /// <summary>
        /// Checks the required attributes in the given entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <exception cref="Exception"></exception>
        [Obsolete("use ValidateRequiredAttributes instead!")]
        public static void Validate<T>(this T entity) where T : class
        {
            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(entity, context, results);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    throw new Exception(validationResult.ErrorMessage);
                }
            }
        }

        /// <summary>
        /// Validates the required attributes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <exception cref="Exception"></exception>
        public static void ValidateRequiredAttributes<T>(this T entity) where T : class
        {
            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(entity, context, results);

            if (!isValid)
            {
                foreach (var validationResult in results)
                {
                    throw new Exception(validationResult.ErrorMessage);
                }
            }
        }
    }
}

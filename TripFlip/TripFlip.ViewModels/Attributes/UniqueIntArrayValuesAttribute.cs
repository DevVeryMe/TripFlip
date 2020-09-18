using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TripFlip.ViewModels.Attributes
{
    /// <summary>
    /// Used to check if int array contains unique values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    class UniqueIntArrayValuesAttribute : ValidationAttribute
    {
        /// <summary>
        /// Returns true if all values of array are unique.
        /// </summary>
        /// <param name="value">The value to test for validity.</param>
        /// <returns><c>true</c> means the <paramref name="value"/> is valid</returns>
        public override bool IsValid(object value)
        {

            // Automatically pass if value is null or empty. RequiredAttribute should be used to assert a value is not empty.
            if (value == null)
            {
                return true;
            }

            var intArray = (value as IEnumerable<int>);

            bool isValid = intArray is null ? false
                : intArray.Distinct().Count() == intArray.Count();

            return isValid;
        }
    }
}

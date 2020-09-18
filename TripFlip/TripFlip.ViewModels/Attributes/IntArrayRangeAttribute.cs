using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TripFlip.ViewModels.Attributes
{
    /// <summary>
    /// Used for specifying an int array range constraint.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    class IntArrayRangeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the minimum value for the range
        /// </summary>
        public int Minimum { get; private set; }

        /// <summary>
        /// Gets the maximum value for the range
        /// </summary>
        public int Maximum { get; private set; }

        /// <summary>
        /// Constructor that takes integer minimum and maximum values
        /// </summary>
        /// <param name="minimum">The minimum value, inclusive</param>
        /// <param name="maximum">The maximum value, inclusive</param>
        public IntArrayRangeAttribute(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        /// <summary>
        /// Returns true if the value falls between min and max, inclusive.
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

            var intValuesArray = value as IEnumerable<int>;

            bool containsInvalidValues = intValuesArray is null ? true
                : intValuesArray.Any(n => n < Minimum || n > Maximum);

            return !containsInvalidValues;
        }
    }
}

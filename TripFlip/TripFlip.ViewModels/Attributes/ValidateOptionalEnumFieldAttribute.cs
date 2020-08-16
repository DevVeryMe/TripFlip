using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class ValidateOptionalEnumFieldAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var type = value.GetType();
            bool isValid = type.IsEnum && Enum.IsDefined(type, value);

            return isValid;
        }
    }
}

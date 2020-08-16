using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class ValidateOptionalEnumFieldAttribute : ValidationAttribute
    {
        private Type _enumType;

        public ValidateOptionalEnumFieldAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var type = value.GetType();

            bool isValid = (type.IsEnum && Enum.IsDefined(type, value) && (type == _enumType));

            return isValid;
        }
    }
}

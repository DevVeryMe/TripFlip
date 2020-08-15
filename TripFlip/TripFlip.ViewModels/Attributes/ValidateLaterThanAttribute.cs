using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class ValidateLaterThanAttribute : ValidationAttribute
    {
        private readonly string _startsAtPropertyName;

        public ValidateLaterThanAttribute(string startsAtPropertyName)
        {
            _startsAtPropertyName = startsAtPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            DateTimeOffset? currentDateTime = value as DateTimeOffset?;

            var comparingDateTimeProperty = validationContext.ObjectType.GetProperty(_startsAtPropertyName);

            if (comparingDateTimeProperty == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            if (comparingDateTimeProperty.GetValue(validationContext.ObjectInstance) is 
                DateTimeOffset startsAtPropertyValue && currentDateTime != null)
            {
                return currentDateTime > startsAtPropertyValue ?
                     ValidationResult.Success :
                     new ValidationResult(ErrorMessage);
            }
                
            return ValidationResult.Success;
        }
    }
}

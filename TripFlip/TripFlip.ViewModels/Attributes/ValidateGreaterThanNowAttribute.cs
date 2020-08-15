using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class ValidateGreaterThanNowAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTimeOffset? startDateTime = value as DateTimeOffset?;
            var nowDateTime = new DateTimeOffset(DateTime.Now);
            var isValid = true;

            if (startDateTime != null)
            {
                isValid = startDateTime > nowDateTime;
            }

            return isValid;
        }
    }
}

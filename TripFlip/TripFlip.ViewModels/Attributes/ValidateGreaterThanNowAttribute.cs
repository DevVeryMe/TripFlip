using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class ValidateGreaterThanNowAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTimeOffset? dateTimeOffset = value as DateTimeOffset?;
            var todayDateTimeOffset = new DateTimeOffset(DateTime.Now);
            var isValid = true;

            if (dateTimeOffset != null)
            {
                isValid = dateTimeOffset > todayDateTimeOffset;
            }

            return isValid;
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class DateIsLessThanNowAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTimeOffset? givenDateTime = value as DateTimeOffset?;
            var currentDateTime = new DateTimeOffset(DateTime.Now);
            var isValid = true;

            if (givenDateTime != null)
            {
                isValid = givenDateTime < currentDateTime;
            }

            return isValid;
        }
    }
}

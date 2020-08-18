using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class ValidateLaterThanNowAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTimeOffset? comparingDateTime = value as DateTimeOffset?;
            var nowDateTime = new DateTimeOffset(DateTime.Now);
            var isValid = true;

            if (comparingDateTime != null)
            {
                isValid = comparingDateTime > nowDateTime;
            }

            return isValid;
        }
    }
}

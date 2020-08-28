using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiUnitTests.RouteViewModels.Helpers
{
    static class ModelValidator
    {
        public static IList<ValidationResult> Validate(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);

            Validator.TryValidateObject(model, validationContext, validationResults, true);

            return validationResults;
        }

        public static bool IsValid(object model)
        {
            var validationContext = new ValidationContext(model, null, null);

            bool isValid = Validator.TryValidateObject(model, validationContext, null, true);

            return isValid;
        }
    }
}

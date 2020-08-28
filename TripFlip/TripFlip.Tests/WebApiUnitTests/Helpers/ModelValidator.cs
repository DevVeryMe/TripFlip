using System.ComponentModel.DataAnnotations;

namespace WebApiUnitTests.Helpers
{
    static class ModelValidator
    {
        /// <summary>
        /// Validates any model object by its validation attributes.
        /// </summary>
        /// <param name="model">Model to validate.</param>
        /// <returns>True if model is valid and false if not.</returns>
        public static bool IsValid(object model)
        {
            var validationContext = new ValidationContext(model, null, null);

            bool isValid = Validator.TryValidateObject(model, validationContext, null, true);

            return isValid;
        }
    }
}

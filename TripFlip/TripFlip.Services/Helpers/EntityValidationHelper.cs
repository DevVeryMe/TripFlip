using System;

namespace TripFlip.Services.Helpers
{
    public static class EntityValidationHelper
    {
        /// <summary>
        /// Validates whether entity is not null. If null,
        /// throws an ArgumentException.
        /// </summary>
        /// <typeparam name="TEntity">Any entity to check.</typeparam>
        /// <param name="entity">Instance of TEntity.</param>
        /// <param name="errorMessage">Error message to display.</param>
        public static void ValidateEntityNotNull<TEntity>(TEntity entity, string errorMessage)
        {
            if (entity is null)
            {
                throw new ArgumentException(errorMessage);
            }
        }
    }
}

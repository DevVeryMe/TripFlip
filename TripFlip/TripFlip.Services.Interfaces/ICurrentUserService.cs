using System;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Interface that describes accessible properties of the current user.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Property that reflects Id of the current user.
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// Property that reflects Email of the current user.
        /// </summary>
        string UserEmail { get; }
    }
}

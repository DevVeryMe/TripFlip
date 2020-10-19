using System.Threading.Tasks;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Service that provides methods for user's Google authentification 
    /// and JWT generation.
    /// </summary>
    public interface IGoogleApiUserService
    {
        /// <summary>
        /// Requests Google's OpenID configuration values. Then prepares and returns a link 
        /// with required parameters in order to authenticate user.
        /// </summary>
        /// <returns>A link to Goolge's OAuth endpoint along with required parameters.</returns>
        Task<string> GetAuthorizationUrlWithParamsAsync();

        /// <summary>
        /// Is responsible for logging user in with the given Google's authorization code.
        /// </summary>
        /// <param name="authorizationCode">A one-time authorization code provided by Google 
        /// that is used to obtain Google's access token, ID token and refresh token.</param>
        /// <returns>A newly generated JWT.</returns>
        Task<string> LoginWithAuthCodeAsync(string authorizationCode);
    }
}

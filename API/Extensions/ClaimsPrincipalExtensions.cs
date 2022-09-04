using System.Security.Claims;

namespace API.Extensions
{
    /// <summary>
    /// Extensions for <see cref="ClaimsPrincipal"/>
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get current username
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetUserName(this ClaimsPrincipal user) => user.FindFirstValue(ClaimTypes.Name);

        /// <summary>
        /// Get current user id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int GetUserId(this ClaimsPrincipal user) => int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}

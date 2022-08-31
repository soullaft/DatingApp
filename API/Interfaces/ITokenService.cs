using API.Entities;

namespace API.Interfaces
{
    /// <summary>
    /// Toker service contract
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Create token to a <see cref="AppUser"/>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string CreateToken(AppUser user);
    }
}

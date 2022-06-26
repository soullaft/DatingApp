using API.Entities;

namespace API.Interfaces
{
    /// <summary>
    /// Toker service contract
    /// </summary>
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}

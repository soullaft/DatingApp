using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository : IRepository<AppUser>
    {
        /// <summary>
        /// Update <see cref="AppUser"/>
        /// </summary>
        /// <param name="user"></param>
        void Update(AppUser user);

        /// <summary>
        /// Save all changes
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveAllAsync();

        /// <summary>
        /// Get <see cref="AppUser"/> by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<AppUser> GetUserByNameAsync(string userName);
    }
}

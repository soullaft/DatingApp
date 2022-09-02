using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Pagination;
using API.Pagination.Params;

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

        /// <summary>
        /// Get all <see cref="IEnumerable{MemberDto}"/>
        /// </summary>
        /// <returns></returns>
        Task<PagedList<MemberDto>> GetAllMembersAsync(UserParams userParams);

        /// <summary>
        /// Get <see cref="MemberDto"/>
        /// </summary>
        /// <returns></returns>
        Task<MemberDto> GetMemberAsync(string userName);
    }
}

using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    /// <summary>
    /// Like repository
    /// </summary>
    public interface ILikesRepository
    {
        /// <summary>
        /// Get <see cref="UserLike"/> of current user to a liked user
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="likedUserId"></param>
        /// <returns></returns>
        Task<UserLike> GetUserLikeAsync(int sourceId, int likedUserId);

        /// <summary>
        /// Get user with likes
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<AppUser> GetUserWithLikesAsync(int userId);

        /// <summary>
        /// Get users likes
        /// </summary>
        /// <param name="likesParams"></param>
        /// <returns></returns>
        Task<PagedList<LikeDto>> GetUserLikesAsync(LikesParams likesParams);
    }
}

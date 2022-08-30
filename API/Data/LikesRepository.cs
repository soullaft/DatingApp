using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _dataContext;
        public LikesRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<UserLike> GetUserLikeAsync(int sourceId, int likedUserId)
        {
            return await _dataContext.Likes.FindAsync(sourceId, likedUserId);
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikesAsync(string predicate, int userId)
        {
            var users  = _dataContext.Users.OrderBy(u => u.UserName).AsQueryable();
            var likes = _dataContext.Likes.AsQueryable();

            if(predicate == MsgConst.LIKE_PREDICATE)
            {
                likes = likes.Where(like => like.SourceUserId == userId);
                users = likes.Select(like => like.LikedUser);
            }

            if(predicate == MsgConst.LIKED_BY_PREDICATE)
            {
                likes = likes.Where(like => like.LikedUserId == userId);
                users = likes.Select(like => like.SourceUser);
            }

            return await users.Select(u => new LikeDto
            {
                UserName = u.UserName,
                KnownAs = u.KnownAs,
                Age = u.DateOfBirth.CalculateAge(),
                PhotoUrl = u.Photos.FirstOrDefault(p => p.IsMain).Url,
                City = u.City,
                Id = u.Id,
            }).ToListAsync();
        }

        public async Task<AppUser> GetUserWithLikesAsync(int userId)
        {
            return await _dataContext.Users.Include(u => u.LikedUsers).FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}

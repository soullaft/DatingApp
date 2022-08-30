using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _dataContext;
        public LikesRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<UserLike> GetUserLikeAsync(int sourceId, int likedUserId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LikeDto>> GetUserLikesAsync(string predicate, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserLike> GetUserWithLikesAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}

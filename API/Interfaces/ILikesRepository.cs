﻿using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLikeAsync(int sourceId, int likedUserId);

        Task<UserLike> GetUserWithLikesAsync(int userId);

        Task<IEnumerable<LikeDto>> GetUserLikesAsync(string predicate, int userId);
    }
}

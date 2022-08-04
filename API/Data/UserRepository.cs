using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync() => await _dataContext.Users.ToListAsync();

        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            return await _dataContext.Users
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AppUser> GetByIdAsync(int id) => await _dataContext.Users.FindAsync(id);

        public async Task<MemberDto> GetMemberAsync(string userName)
        {
            return await _dataContext.Users
                .Where(user => user.UserName == userName)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<AppUser> GetUserByNameAsync(string userName) => await _dataContext.Users.Include(p => p.Photos).SingleOrDefaultAsync(user => user.UserName == userName);

        public async Task<bool> SaveAllAsync() => await _dataContext.SaveChangesAsync() > 0;

        public void Update(AppUser user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
        }
    }
}

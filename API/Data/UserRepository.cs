using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Pagination;
using API.Pagination.Params;
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

        public async Task<PagedList<MemberDto>> GetAllMembersAsync(UserParams userParams)
        {
            var query = _dataContext.Users
                .AsQueryable();

            query = query.Where(u => u.UserName != userParams.CurrentUsername);
            query = query.Where(u => u.Gender == userParams.Gender);

            var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

            query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(u => u.LastActive),
            };

            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(), 
                userParams.PageNumber, userParams.PageSize);
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

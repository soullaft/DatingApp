using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync() => await _dataContext.Users.ToListAsync();

        public async Task<AppUser> GetByIdAsync(int id) => await _dataContext.Users.FindAsync(id);

        public async Task<AppUser> GetUserByNameAsync(string userName) => await _dataContext.Users.SingleOrDefaultAsync(user => user.UserName == userName);

        public async Task<bool> SaveAllAsync() => await _dataContext.SaveChangesAsync() > 0;

        public void Update(AppUser user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
        }
    }
}

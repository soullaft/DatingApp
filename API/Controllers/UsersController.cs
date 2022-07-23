using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /// <summary>
    /// Represents all actions with the users
    /// </summary>
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly DataContext context;

        public UsersController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() 
        {
            var users = await context.Users.ToListAsync();

            if(!users.Any())
                return new List<AppUser>
                {
                    new AppUser()
                    {
                        Id = 1,
                        UserName = "Soullaft"
                    }
                };
            
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id) => await context.Users.FindAsync(id);
    }
}

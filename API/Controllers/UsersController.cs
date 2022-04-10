using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext context;

        public UsersController(DataContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        //default get method
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() 
        {
            var users = await context.Users.ToListAsync();

            if(!users.Any())
                return new List<AppUser>
                {
                    new AppUser()
                    {
                        Id = 228,
                        UserName = "Soullaft"
                    }
                };
            
            return users;
        }

        [Authorize]
        //api/users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id) => await context.Users.FindAsync(id);
    }
}

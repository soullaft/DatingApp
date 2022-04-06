using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext context;

        public UsersController(DataContext context)
        {
            this.context = context;
        }

        //default get method
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() => await context.Users.ToListAsync();

        //api/users/id
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id) => await context.Users.FindAsync(id);
    }
}

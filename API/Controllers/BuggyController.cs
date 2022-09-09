using API.Data;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Throw;

namespace API.Controllers
{
    /// <summary>
    /// Only for testing server errors during requests
    /// </summary>
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<string> GetNotFound()
        {
            var incorrectUser = _context.Users.Find(-1);

            incorrectUser.ThrowIfNull();

            return Ok(incorrectUser);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Users.Find(-1);

            var thingToReturn = thing.ToString();

            return thingToReturn;
        }
    }
}

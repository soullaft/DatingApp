using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    /// <summary>
    /// Register and login account controller
    /// </summary>
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto)
        {
            //if user is already exist throw an error
            if (await IsUserExist(registerDto.Username))
                return BadRequest("Username is taken");

            //create hmac with random key
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.UserName == loginDto.Username);

            if (user == null)
                return BadRequest("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        /// <summary>
        /// Is user with specified name already exist
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        private async Task<bool> IsUserExist(string username) => await _context.Users.AnyAsync(user => user.UserName == username.ToLower());
    }
}
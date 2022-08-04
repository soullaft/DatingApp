using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Extensions.DataAnnotaions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace API.Controllers
{
    /// <summary>
    /// Represents all actions with the users
    /// </summary>
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService = photoService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() 
        {
            var users = await _userRepository.GetAllMembersAsync();

            return Ok(users);
        }

        /// <summary>
        /// Get user by userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string userName) => await _userRepository.GetMemberAsync(userName);

        /// <summary>
        /// Update user to memberUpdateDto <see cref="MemberUpdateDto"/>
        /// </summary>
        /// <param name="memberUpdateDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _userRepository.GetUserByNameAsync(User.GetUserName());

            _mapper.Map(memberUpdateDto, user);

            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }

        /// <summary>
        /// Add photo to current user
        /// </summary>
        /// <param name="formFile">file</param>
        /// <returns></returns>
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto([AllowedExtensions] IFormFile formFile)
        {
            var user = await _userRepository.GetUserByNameAsync(User.GetUserName());
            
            var result = await _photoService.AddPhotoAsync(formFile);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            //if we have 0 photos for user then we should make this added photo as main
            if(user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if(await _userRepository.SaveAllAsync())
            {
                return _mapper.Map<Photo, PhotoDto>(photo);
            }

            return BadRequest("Problem problem");
        }
    }
}

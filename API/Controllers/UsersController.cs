using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Extensions.DataAnnotaions;
using API.Helpers;
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
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserParams userParams) 
        {
            var user = await _userRepository.GetUserByNameAsync(User.GetUserName());
            userParams.CurrentUsername = user.UserName;

            if (string.IsNullOrEmpty(userParams.Gender))
                userParams.Gender = user.Gender == "male" ? "female" : "male";

            var users = await _userRepository.GetAllMembersAsync(userParams);

            Response.AddPaginationHeader(new PaginationHeader()
            {
                CurrentPage = users.CurrentPage,
                ItemsPerPage = users.PageSize,
                TotalItems = users.TotalCount,
                TotalPages = users.TotalPages
            });

            return Ok(users);
        }

        /// <summary>
        /// Get user by userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("{username}", Name = "GetUser")]
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
        /// <param name="file">file</param>
        /// <returns></returns>
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto([AllowedExtensions] IFormFile file)
        {
            var user = await _userRepository.GetUserByNameAsync(User.GetUserName());
            
            var result = await _photoService.AddPhotoAsync(file);

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
                var photoDto = _mapper.Map<Photo, PhotoDto>(photo);
                return CreatedAtRoute("GetUser",new { userName = user.UserName}, photoDto);
            }

            return BadRequest("Problem problem");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _userRepository.GetUserByNameAsync(User.GetUserName());

            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);

            if (photo.IsMain) return BadRequest("This is already main photo");

            var currentMainPhoto = user.Photos.FirstOrDefault(p => p.IsMain);

            if (currentMainPhoto == null) return BadRequest("Something went wrong");

            currentMainPhoto.IsMain = false;
            photo.IsMain = true;

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _userRepository.GetUserByNameAsync(User.GetUserName());

            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);

            if(photo == null) return NotFound();

            if (photo.IsMain) return BadRequest("You can't delete main photo");

            if(photo.PublicId != null)
            {
                var photoDeleteResult = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (photoDeleteResult.Error != null) return BadRequest(photoDeleteResult.Error.Message);
            }

            user.Photos.Remove(photo);

            if(await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete photo");
        }
    }
}

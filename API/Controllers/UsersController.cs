using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /*
    [ApiController]  =>we dont need those 2 attributes anymore because we are inheriting from BaseApiController class which has the same attributes
    [Route("api/[controller]")]
    */
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
       
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository,IMapper mapper, IPhotoService photoService )
        {
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() //we use task and sync becasue this was synchronous code which means when we query something for database user have to wait for thread to response because there can be multiple users trying to reach same endpoint 
        {
           // return await _context.Users.ToListAsync(); //when a request to database goes this code waits and it returns when the task is done 
           var users = await _userRepository.GetMembersAsync();

            return Ok(users); 
        }

        //api/users/id so when user will interact with this endpoint it will return the whats in the method
        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return  await _userRepository.getMemberAsync(username);
            //return _mapper.Map<MemberDto>(user);
            
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            _mapper.Map(memberUpdateDto, user);
            _userRepository.Update(user);

            if(await _userRepository.SaveAllAsync()) return NoContent(); //we dont need to send a content for a put request

            return BadRequest("failed to update the user.");

        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var result = await _photoService.AddPhotoAsync(file);

            if(result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if(user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if(await _userRepository.SaveAllAsync())
            {

              //  return _mapper.Map<PhotoDto>(photo); //this one will return 200 OK response 
              return CreatedAtRoute("GetUser",new {username = user.UserName} ,_mapper.Map<PhotoDto>(photo) ); //rhis will return 201 Created response, second overload is to specify that we need the username for parameter in getUser controller 
            }else
            {
                return BadRequest("Problem adding photo");
            }
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername()); 

            var photo = user.Photos.FirstOrDefault( x=>x.Id == photoId);
            if(photo.IsMain) return BadRequest("This is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if(currentMain != null) currentMain.IsMain = false;
            photo.IsMain =true;
            
            if(await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set main photo");

        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if(photo == null) return NotFound();

            if(photo.IsMain) return BadRequest("You cannot delete your main photo");

            if(photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if(result.Error !=null ) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if(await _userRepository.SaveAllAsync()) return Ok();

            return  BadRequest("failed to delete the photo");

        }

    }
}
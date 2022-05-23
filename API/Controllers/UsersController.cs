using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
       
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository,IMapper mapper )
        {
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
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return  await _userRepository.getMemberAsync(username);
            //return _mapper.Map<MemberDto>(user);
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /*
    [ApiController]  =>we dont need those 2 attributes anymore because we are inheriting from BaseApiController class which has the same attributes
    [Route("api/[controller]")]
    */
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() //we use task and sync becasue this was synchronous code which means when we query something for database user have to wait for thread to response because there can be multiple users trying to reach same endpoint 
        {
            return await _context.Users.ToListAsync(); //when a request to database goes this code waits and it returns when the task is done 
            
        }

        //api/users/id so when user will interact with this endpoint it will return the whats in the method
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
            
        }
    }
}
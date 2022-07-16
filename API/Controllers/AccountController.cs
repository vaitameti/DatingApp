using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(DataContext context, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")] //we use httppost when we want to add new resource through our API endpoint
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) 
        {
            if(await UserExists(registerDto.Username)) return BadRequest("Username is taken"); //bad request comes from actionresult task
            //we use using statement when we want to dispose the method after we use it, there is an interface inside this method which u can see by pressing f12 to see Idisposable class
            
            var user = _mapper.Map<AppUser>(registerDto);
            
            using var hmac = new HMACSHA512();

          
            
            user.UserName = registerDto.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password));//we use encoding becuase ComputeHash method gets bytearray as input and our password is string so basically we change it to byte array
            user.PasswordSalt = hmac.Key; //we save the key number to password salt
            
            _context.Users.Add(user);//we dont save this to our database this is for tracking
            await _context.SaveChangesAsync(); //with this line we save the user into our database

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs

            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .Include(p=>p.Photos) 
                .SingleOrDefaultAsync(x=> x.UserName == loginDto.Username);
            if(user == null) return Unauthorized("Invalid username");

            using var hmac =new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < computedHash.Length;i++)
            {
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}
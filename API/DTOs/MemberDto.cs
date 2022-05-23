using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class MemberDto
    {
        public int id { get; set; } //if we set protected it means this and child class can acces
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        public int Age { get; set; } 
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime Active { get; set; }
        public string Gender { get; set; } 
        public string introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interest { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}
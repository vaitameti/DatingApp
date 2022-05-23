using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;

namespace API.Entities
{
    public class AppUser // becuase we are going to use this class with entity framework we are going to give it public modifyer 
    {
        public int id { get; set; } //if we set protected it means this and child class can acces
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }//we are storing our hash and salt  in byte array
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; } 
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Active { get; set; } = DateTime.Now;
        public string Gender { get; set; } 
        public string introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interest { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }

        //public int GetAge()
        //{
        //    return DateOfBirth.CalculateAge();
        //}
    }
}
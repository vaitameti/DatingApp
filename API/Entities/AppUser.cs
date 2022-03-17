using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser // becuase we are going to use this class with entity framework we are going to give it public modifyer 
    {
        public int id { get; set; } //if we set protected it means this and child class can acces
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }//we are storing our hash and salt  in byte array
        public byte[] PasswordSalt { get; set; }

    }
}
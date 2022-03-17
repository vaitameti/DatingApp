using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
        //any class that implements interface means it will implment interfaces properties,methods and all events
        //an interface doesnt contain implementation logic,only contains signature of functionalities interface provide
        //we dont have to create interface for a webtoken it might work same with the class, but for testing reasons
        //we will create single method signature
        string CreateToken(AppUser user);
    }
}
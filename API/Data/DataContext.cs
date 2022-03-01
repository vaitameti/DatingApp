using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Entities;// this is for DbSet AppUser class

namespace API.Data
{
    public class DataContext : DbContext //deriving from DbContext class which came along with entity framework
    {
        public DataContext(DbContextOptions options) : base(options) //the options came from Dbcontext,when we created constructor using the lightbulbp it gave us theoption to select --generate constructor(options)
        {
        }

        public DbSet<AppUser> Users { get; set; } /*type DbSet, this takes the typeof the class that we want to create a database set for.And in this case, it's our AppUser class. and the User is what our table called inside that class*/
    }
}
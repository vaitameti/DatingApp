using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
             // we are using scoped mostly for http requests, when we ask for a request it will inizialize addscoped and once the request will be delivered it will stop
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            //connection string for our database
            //we are using the<DataContext> type which derived from DbContext we shouldnt use<DbContext>type
            services.AddDbContext<DataContext>(options => //=> this is a lambda expression(the options is a parameter that we pass to a statement block which is below this code )
            {
                //below method is connecting to our database 
                //options.UseSqlite("Connection string");//if we want to pass an expression as a parameter we use lambda expressions
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
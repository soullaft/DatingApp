using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;
using API.Helpers;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        /// <summary>
        /// Extension of configure all using application services
        /// </summary>
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //create data context depending on credentials in config file
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            #region DI
            
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            #endregion

            //make all http request to redirect to https protocol
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
                options.HttpsPort = 443;
            });

            services.AddControllers();
            services.AddCors();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}

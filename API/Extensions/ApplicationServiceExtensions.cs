using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;
using API.Helpers;
using API.Filters;

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
                options.UseSqlite(configuration.GetConnectionString(MsgConst.RESOURCE_CONNECTION_SETTINGS));
            });

            services.Configure<CloudinarySettings>(configuration.GetSection(MsgConst.RESOURCE_CLOUDINARY_SETTINGS));

            #region DI

            services.AddScoped<LogUserActivity>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
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

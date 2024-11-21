using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;


namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
       // Extension method to add application services to the IServiceCollection
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Configure the application's DbContext to use a SQLite database
            services.AddDbContext<DataContext>(opt => 
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")); // Use the connection string named "DefaultConnection" from the configuration
            });
            
            // Add CORS (Cross-Origin Resource Sharing) services to the application
            services.AddCors();

            // Register the TokenService as the implementation for the ITokenService interface
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

            services.AddScoped<IPhotoService, PhotoService>();

            // Return the service collection to allow for method chaining
            return services;
        }
    }
}